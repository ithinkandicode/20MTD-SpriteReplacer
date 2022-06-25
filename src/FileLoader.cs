using System;
using System.IO;
using UnityEngine;
using System.Linq;
using SpriteReplacer.AssetStore;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    internal static class FileLoader
    {

        //SourceDirectory = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Textures", configTextureModFolders.Value);
        internal static bool LoadTextures(List<string> modFolders)
        {
            foreach (string folder in modFolders)
            {
                string modDir = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Textures", folder);
                try
                {
                    foreach (string filepath in Directory.EnumerateFiles(modDir, "*.*", SearchOption.AllDirectories))
                    {
                        Log.LogDebug("Found file " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath.Replace(modDir + "\\", ".\\"));
                        Texture2D texture2D = new Texture2D(2, 2, GraphicsFormat.R8G8B8A8_UNorm, 1, TextureCreationFlags.None);
                        texture2D.LoadImage(File.ReadAllBytes(filepath));
                        TextureStore.textureDict.Add(Path.GetFileNameWithoutExtension(filepath), texture2D);
                    }
                }
                catch (Exception e)
                {
                    Log.LogError("Error loading Textures. Please make sure you configured the mod folders correctly.");
                    Log.LogError(e.GetType() + " " + e.Message);
                    return false;
                }
            }
            Log.LogInfo("Textures loaded successfully.");
            return true;
        }
    }
}