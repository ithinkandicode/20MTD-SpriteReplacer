using UnityEngine;
using System.Collections.Generic;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer.AssetStore
{
    public static class SpriteStore
    {
        internal static Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();
        public static List<Sprite> changedList = new List<Sprite>();

        internal static void Init()
        {
            // FileLoader.loadSprites(modDirectories); not implemented yet
        }

        public static void LogAll()
        {
            foreach (KeyValuePair<string, Sprite> entry in spriteDict)
            {
                Log.LogInfo("TextureDict Entry: " + entry.Key + " | " + entry.Value);
            }
        }
    }
}