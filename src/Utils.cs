using System;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using AssetReplacer.AssetStore;
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
                    Graphics.CopyTexture(TextureStore.textureDict[ogSprite.texture.name], ogSprite.texture);
                    // SpriteStore.changedList.Add(ogSprite);
                    Log.LogDebug("OK! Replaced Texture " + ogSprite.texture.name + " for Sprite " + ogSprite.name);
                    return true;
                }
                else
                {
                    Log.LogDebug("FAIL! No Texture available for " + ogSprite.texture.name);
                }
            }
            return false;
        }

        internal static bool TryReplaceAudioClip(AudioClip audioClip)
        {
            Log.LogDebug($"[Music] Attempting to load custom music ({audioClip.name})...");

            if (AudioStore.audioDict.ContainsKey(audioClip.name))
            {
                AudioClip modClip = AudioStore.audioDict[audioClip.name];
                float[] samples = new float[modClip.samples * modClip.channels];
                modClip.GetData(samples, 0);
                audioClip.SetData(samples, 0);
                Log.LogDebug("[Music] Success. Applying custom music track!");
                return true;
            }
            else
            {
                Log.LogDebug($"[Music] Failed. Could not load the music track ({audioClip.name})");
                return false;
            }
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
        public static async Task<AudioClip> LoadMusicFromDisk(string SourceMusicDirectory, string musicFilename, AudioType audioType)
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
    }
}
