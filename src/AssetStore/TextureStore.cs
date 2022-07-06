using UnityEngine;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer.AssetStore
{
    public static class TextureStore
    {
        internal static Dictionary<string, Texture2D> TextureDict = new Dictionary<string, Texture2D>();
        public static List<Texture2D> ChangedList = new List<Texture2D>();

        internal static void Init()
        {
            FileLoader.LoadTextures();
        }

        public static void LogAll()
        {
            Log.LogInfo("Logging all TextureStore entries.");
            foreach (KeyValuePair<string, Texture2D> entry in TextureDict)
            {
                Log.LogInfo("TextureDict Entry: " + entry.Key + " | " + entry.Value);
            }
        }
    }
}