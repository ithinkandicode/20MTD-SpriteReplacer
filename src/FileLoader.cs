using System;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using AssetReplacer.AssetStore;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer
{
    internal static class FileLoader
    {
        public static List<string> TextureModFolders = new List<string>();
        public static List<string> AudioModFolders = new List<string>();
        public static List<string> AssetBundleModFolders = new List<string>();
        public static List<string> SpriteAnimationModFolders = new List<string>();

        internal static void LoadTextures()
        {
            try
            {
                foreach (string modName in TextureModFolders)
                {
                    string textureDir = getAssetDir(modName, "Textures");

                    foreach (string filepath in Directory.EnumerateFiles(textureDir, "*.*", SearchOption.AllDirectories))
                    {
                        Log.LogDebug("Found Texture2D " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath.Replace(textureDir + "\\", ".\\"));
                        Texture2D texture2D = new Texture2D(2, 2, GraphicsFormat.R8G8B8A8_UNorm, 1, TextureCreationFlags.None);
                        texture2D.LoadImage(File.ReadAllBytes(filepath));
                        TextureStore.TextureDict[Path.GetFileNameWithoutExtension(filepath)] = texture2D;
                    }
                }
            }
            catch (Exception e)
            {
                Log.LogError("Error loading Textures. Please make sure you configured the mod folders correctly and it doesn't contain any unrelated files.");
                Log.LogError(e.GetType() + " " + e.Message);
            }
            Log.LogInfo("Textures loaded successfully.");
        }

        internal static async Task<bool> LoadAudio()
        {
            try
            {
                foreach (string modName in AudioModFolders)
                {
                    string audioDir = getAssetDir(modName, "Audio");

                    foreach (string filepath in Directory.EnumerateFiles(audioDir, "*.*", SearchOption.AllDirectories))
                    {

                        Log.LogDebug("Found Audio " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath.Replace(audioDir + "\\", ".\\"));
                        AudioClip audioClip = await Utils.LoadMusicFromDisk(audioDir, Path.GetFileName(filepath), AudioType.MPEG);
                        AudioStore.AudioDict[Path.GetFileNameWithoutExtension(filepath)] = audioClip;
                    }
                }
            }
            catch (Exception e)
            {
                Log.LogError("Error loading Audio. Please make sure you configured the mod folders correctly and it doesn't contain any unrelated files.");
                Log.LogError(e.GetType() + " " + e.Message);
                return false;
            }
            Log.LogInfo("Audio loaded successfully.");
            return true;
        }

        internal static void LoadAssetbundles()
        {
            try
            {
                foreach (string modname in AssetBundleModFolders)
                {
                    string assetBundleDir = getAssetDir(modname, "Assetbundles");
                    foreach (string filepath in Directory.EnumerateFiles(assetBundleDir, "*.*", SearchOption.AllDirectories))
                    {

                        Log.LogDebug("Found AssetBundle " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath.Replace(assetBundleDir + "\\", ".\\"));
                        AssetBundle assetBundle = AssetBundle.LoadFromFile(filepath);
                        AssetBundleStore.AssetbundleDict[Path.GetFileNameWithoutExtension(filepath)] = assetBundle;
                    }
                }
            }
            catch (Exception e)
            {
                Log.LogError("Error loading AssetBundle. Please make sure you configured the mod folders correctly and it doesn't contain any unrelated files.");
                Log.LogError(e.GetType() + " " + e.Message);
            }
            Log.LogInfo("AssetBundles loaded successfully.");
        }

        internal static void LoadSpriteAnimations()
        {
            try
            {
                foreach (string modname in SpriteAnimationModFolders)
                {
                    string spriteAnimationDir = getAssetDir(modname, "SpriteAnimations");
                    Dictionary<string, string> files = new Dictionary<string, string>();
                    //get all files
                    foreach (string filepath in Directory.EnumerateFiles(spriteAnimationDir, "*.*", SearchOption.AllDirectories))
                    {
                        // files.Add(Path.GetFileNameWithoutExtension(filepath), filepath);
                        String[] nameSplit = Path.GetFileNameWithoutExtension(filepath).Split('_');
                        if (nameSplit.Length == 2)
                        {
                            string name = nameSplit[0];

                            if (!SpriteAnimationStore.SpriteAnimationDict.ContainsKey(name))
                            {
                                SpriteAnimationStore.SpriteAnimationDict.Add(name, new List<Sprite>());
                            }
                            List<Sprite> animList = SpriteAnimationStore.SpriteAnimationDict[name];

                            Texture2D texture2D = new Texture2D(2, 2, GraphicsFormat.R8G8B8A8_UNorm, 1, TextureCreationFlags.None);
                            texture2D.LoadImage(File.ReadAllBytes(filepath));
                            texture2D.name = Path.GetFileNameWithoutExtension(filepath);
                            Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
                            sprite.name = Path.GetFileNameWithoutExtension(filepath);
                            animList.Add(sprite);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.LogError("Error loading SpriteAnimations. Please make sure you configured the mod folders correctly and it doesn't contain any unrelated files.");
                Log.LogError(e.GetType() + " " + e.Message);
            }
            Log.LogInfo("SpriteAnimations loaded successfully.");
        }

        private static string getAssetDir(string modName, string assetType)
        {
            switch (ConfigFolderStructure.Value)
            {
                case "Thunderstore":
                    return Path.Combine(BepInEx.Paths.PluginPath, modName, assetType);
                case "Legacy":
                    return Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", assetType, modName);
                default:
                    Log.LogError("Unsupported folderstructure configured.");
                    return "";
            }
        }
    }
}