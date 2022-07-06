using UnityEngine;
using System.Collections.Generic;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer.AssetStore
{
    public static class AssetbundleStore
    {
        internal static Dictionary<string, AssetBundle> AssetbundleDict = new Dictionary<string, AssetBundle>();

        internal static void Init()
        {
            FileLoader.LoadAssetbundles();
        }

        public static void LogAll()
        {
            Log.LogInfo("Logging all AssetbundleStore entries.");
            foreach (KeyValuePair<string, AssetBundle> entry in AssetbundleDict)
            {
                Log.LogInfo("AssetbundleDict Entry: " + entry.Key + " | " + entry.Value);
            }
        }

        public static void LoadAll(AssetBundle assetBundle)
        {
            Object[] assets = assetBundle.LoadAllAssets();
            foreach (Object asset in assets)
            {
                switch (asset)
                {
                    case Texture2D t:
                        TextureStore.TextureDict.Add(t.name, t);
                        break;
                    case AudioClip a:
                        AudioStore.AudioDict.Add(a.name, a);
                        break;
                    default:
                        Log.LogError("Unsupported asset: " + asset.name);
                        break;
                }
            }
        }
    }
}