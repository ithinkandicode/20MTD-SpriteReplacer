using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    public static class SpriteInfo
    {
        private static Dictionary<string, string> SpriteDict = new Dictionary<string, string>();

        internal static void Init()
        {
            foreach (string filepath in Directory.EnumerateFiles(SourceSpritesDirectory, "*.*", SearchOption.AllDirectories).Select(filepath => filepath.Replace(SourceSpritesDirectory + "\\", "")))
            {
                //@todo: Uncomment this
                //Log.LogDebug("Found file " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath);
                SpriteDict.Add(Path.GetFileNameWithoutExtension(filepath), filepath);
            }
        }

        public static string GetFilePath(string spriteName)
        {
            if (SpriteDict.ContainsKey(spriteName))
            {
                return SpriteDict[spriteName];
            }
            else
            {
                return "";
            }
        }

        public static void LogAll()
        {
            foreach (KeyValuePair<string, string> entry in SpriteDict)
            {
                Log.LogInfo("SpriteDict Entry: " + entry.Key + " | " + entry.Value);
            }
        }

    }
}