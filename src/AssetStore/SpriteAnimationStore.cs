using UnityEngine;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer.AssetStore
{
    public static class SpriteAnimationStore
    {
        internal static Dictionary<string, List<Sprite>> SpriteAnimationDict = new Dictionary<string, List<Sprite>>();

        internal static void Init()
        {
            FileLoader.LoadSpriteAnimations();
        }

        public static void LogAll()
        {
            Log.LogInfo("Logging all SpriteAnimationStore entries.");
            foreach (KeyValuePair<string, List<Sprite>> entry in SpriteAnimationDict)
            {
                Log.LogInfo("SpriteAnimationDict Entry: " + entry.Key + " | " + entry.Value.ToString());
            }
        }
    }
}