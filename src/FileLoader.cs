using System;
using System.IO;
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

        internal static void LoadTextures()
        {
            foreach (string modName in TextureModFolders)
            {
                string textureDir = getAssetDir(modName, "Textures");
                try
                {
                    foreach (string filepath in Directory.EnumerateFiles(textureDir, "*.*", SearchOption.AllDirectories))
                    {
                        Log.LogDebug("Found file " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath.Replace(textureDir + "\\", ".\\"));
                        Texture2D texture2D = new Texture2D(2, 2, GraphicsFormat.R8G8B8A8_UNorm, 1, TextureCreationFlags.None);
                        texture2D.LoadImage(File.ReadAllBytes(filepath));
                        TextureStore.textureDict[Path.GetFileNameWithoutExtension(filepath)] = texture2D;
                    }
                }
                catch (Exception e)
                {
                    Log.LogError("Error loading Textures. Please make sure you configured the mod folders correctly and it doesn't contain any unrelated files.");
                    Log.LogError(e.GetType() + " " + e.Message);
                }
            }
            Log.LogInfo("Textures loaded successfully.");
        }

        internal static async Task<bool> LoadAudio()
        {
            foreach (string modName in AudioModFolders)
            {
                string audioDir = getAssetDir(modName, "Audio");
                try
                {
                    foreach (string filepath in Directory.EnumerateFiles(audioDir, "*.*", SearchOption.AllDirectories))
                    {
                        Log.LogDebug("Found file " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath.Replace(audioDir + "\\", ".\\"));
                        AudioClip audioClip = await Utils.LoadMusicFromDisk(audioDir, Path.GetFileName(filepath), AudioType.MPEG);
                        AudioStore.audioDict[Path.GetFileNameWithoutExtension(filepath)] = audioClip;
                    }
                }
                catch (Exception e)
                {
                    Log.LogError("Error loading Audio. Please make sure you configured the mod folders correctly and it doesn't contain any unrelated files.");
                    Log.LogError(e.GetType() + " " + e.Message);
                    return false;
                }
            }
            Log.LogInfo("Audio loaded successfully.");
            return true;
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