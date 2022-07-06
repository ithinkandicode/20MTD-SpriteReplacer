using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer.AssetStore
{
    public static class AudioStore
    {
        internal static Dictionary<string, AudioClip> AudioDict = new Dictionary<string, AudioClip>();
        public static List<AudioClip> ChangedList = new List<AudioClip>();
        public static Task<bool> TaskInit;

        internal static void Init()
        {
            TaskInit = FileLoader.LoadAudio();
        }

        public static void LogAll()
        {
            Log.LogInfo("Logging all AudioStore entries.");
            foreach (KeyValuePair<string, AudioClip> entry in AudioDict)
            {
                Log.LogInfo("AudioDict Entry: " + entry.Key + " | " + entry.Value);
            }
        }
    }
}