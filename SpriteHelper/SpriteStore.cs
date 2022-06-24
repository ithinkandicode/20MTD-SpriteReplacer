using System.Collections.Generic;
using UnityEngine;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    public static class SpriteStore
    {
        internal static List<Sprite> ChangedSprites = new List<Sprite>();

        internal static void CleanList()
        {
            ChangedSprites.RemoveAll(item => item == null);
        }

        public static void LogAll()
        {
            foreach (Sprite sprite in ChangedSprites)
            {
                if (sprite != null)
                {
                    Log.LogInfo(sprite.name);
                }
                else
                {
                    Log.LogInfo("null");
                }
            }
        }
    }
}