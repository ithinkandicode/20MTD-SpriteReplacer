using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using AssetReplacer.AssetStore;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer
{
    internal class Utils
    {
        // Returns true on successful patch, false otherwise
        internal static bool TryReplaceTexture2D(Sprite ogSprite)
        {
            if (ogSprite is not null && ogSprite.texture is not null)
            {
                if (/*!SpriteStore.changedList.Contains(ogSprite) && */TextureStore.textureDict.ContainsKey(ogSprite.texture.name))
                {
                    Texture2D tex = TextureStore.textureDict[ogSprite.texture.name];

                    if (ogSprite.texture.format != tex.format)
                    {
                        Log.LogDebug($"INFO! Remaking texture {ogSprite.texture.name}, wants format {ogSprite.texture.format}, have format {tex.format}");

                        //https://docs.unity3d.com/ScriptReference/Texture2D.SetPixels.html
                        List<TextureFormat> validFormats = new List<TextureFormat>(){
                            TextureFormat.Alpha8,
                            TextureFormat.ARGB32,
                            TextureFormat.ARGB4444,
                            TextureFormat.BGRA32,
                            TextureFormat.R16,
                            TextureFormat.R8,
                            TextureFormat.RFloat,
                            TextureFormat.RG16,
                            TextureFormat.RG32,
                            TextureFormat.RGB24,
                            TextureFormat.RGB48,
                            TextureFormat.RGB565,
                            TextureFormat.RGB9e5Float,
                            TextureFormat.RGBA32,
                            TextureFormat.RGBA4444,
                            TextureFormat.RGBA64,
                            TextureFormat.RGBAFloat,
                            TextureFormat.RGBAHalf,
                            TextureFormat.RGFloat,
                            TextureFormat.RGHalf,
                            TextureFormat.RHalf
                        };

                        if (validFormats.Contains(ogSprite.texture.format))
                        {
                            Texture2D newTex = new Texture2D(tex.width, tex.height, ogSprite.texture.format, 1, false);
                            newTex.SetPixels(tex.GetPixels());
                            newTex.Apply();

                            TextureStore.textureDict[ogSprite.texture.name] = newTex;
                            tex = newTex;
                        } else {
                            Log.LogDebug("Failed to remake texture. Invalid format: " + Enum.GetName(typeof(TextureFormat), ogSprite.texture.format));
                        }
                    }
                    if (tex.width == ogSprite.texture.width && tex.height == ogSprite.texture.height && tex.format == ogSprite.texture.format)
                    {
                        Graphics.CopyTexture(tex, ogSprite.texture);
                        Log.LogDebug("OK! Replaced Texture " + ogSprite.texture.name + " for Sprite " + ogSprite.name);
                        return true;
                    }
                    else
                    {
                        Log.LogError($"Failed to replace texture {ogSprite.texture.name} because of dimension or format mismatch. Original Texture: {ogSprite.texture.width}w x {ogSprite.texture.height}h {Enum.GetName(typeof(TextureFormat), ogSprite.texture.format)}, Replacement Texture: {tex.width}w x {tex.height}h {Enum.GetName(typeof(TextureFormat), tex.format)}");
                    }
                }
                else
                {
                    Log.LogDebug("FAIL! No Texture available for " + ogSprite.texture.name);
                }
            }
            return false;
        }

        //TODO: integrate into FileLoader
        /**
        * References » Unity:
        *   https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Networking.UnityWebRequest.html
        *   https://docs.unity3d.com/2019.4/Documentation/ScriptReference/Networking.UnityWebRequestMultimedia.GetAudioClip.html
        *
        * Asynchronously load a music track
        * 
        * Example: await LoadMusicFromDisk("title", AudioType.MPEG);
        * Available audio types: https://docs.unity3d.com/2019.4/Documentation/ScriptReference/AudioType.html
        * Based heavily on: https://github.com/TormentedEmu/7DTD-A19-DMTMods/blob/master/TE_MenuMusic/Harmony/Harmony.cs
        */
        internal static async Task<AudioClip> LoadMusicFromDisk(string SourceMusicDirectory, string musicFilename, AudioType audioType)
        {
            AudioClip audioClip = null;
            string musicPath = Path.Combine("file:///", SourceMusicDirectory, musicFilename);

            Log.LogDebug($"Loading music (title.mp3) from: {musicPath}");

            using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(musicPath, audioType))
            {
                webRequest.SendWebRequest();

                try
                {
                    while (!webRequest.isDone)
                    {
                        await Task.Delay(5);
                    }

                    if (webRequest.isNetworkError || webRequest.isHttpError)
                    {
                        Log.LogDebug(webRequest.error);
                        Log.LogDebug("Music file retrieval failed (network error)");
                    }
                    else
                    {
                        audioClip = DownloadHandlerAudioClip.GetContent(webRequest);
                        audioClip.name = "Custom " + musicFilename;
                    }
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    Log.LogError("Failed to retrieve music file (general error).");
                }
            }

            return audioClip;
        }

        internal static bool ApplyDynamicFogOfWar(Transform fogOfWarCanvas)
        {
            Transform child = fogOfWarCanvas.GetChild(0);

            if (child == null)
            {
                Debug.LogError("Fog of war canvas has no child!");
                return false;
            }

            RawImage img = child.GetComponent<RawImage>();

            if (img == null)
            {
                Debug.LogError("Fog of war image has no RawImage component!");
                return false;
            }

            Texture2D grasstile = null;
            if (!TextureStore.textureDict.TryGetValue("T_TileGrass", out grasstile))
                return false;

            Color[] colors = grasstile.GetPixels();

            Color additive = Color.black;

            foreach (Color color in colors)
            {
                additive += color;
            }

            Color average = additive / colors.Length * 0.75f;

            Material material = img.material;
            material.color = average;
            return true;
        }
    }
}
