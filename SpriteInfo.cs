using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    static class SpriteInfo
    {
        private static Dictionary<string, string> SpriteDict = new Dictionary<string, string>();

        public static void Init()
        {
            foreach (string filepath in Directory.EnumerateFiles(SourceDirectory, "*.*", SearchOption.AllDirectories).Select(filepath => filepath.Replace(SourceDirectory + "\\", "")))
            {
                Log.LogDebug("Found file " + Path.GetFileNameWithoutExtension(filepath) + " at " + filepath);
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

    }
}