using UnityEngine;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer.AssetStore
{
    public static class SpriteStore
    {
        internal static Dictionary<string, Sprite> SpriteDict = new Dictionary<string, Sprite>();
        public static List<Sprite> ChangedList = new List<Sprite>();

        internal static void Init()
        {
            // FileLoader.loadSprites(); not implemented yet
        }

        public static void LogAll()
        {
            Log.LogInfo("Logging all SpriteStore entries.");
            foreach (KeyValuePair<string, Sprite> entry in SpriteDict)
            {
                Log.LogInfo("TextureDict Entry: " + entry.Key + " | " + entry.Value);
            }
        }
    }
}