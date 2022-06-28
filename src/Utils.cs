using flanne;
using UnityEngine;
using AssetReplacer.AssetStore;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer
{
    internal class Utils
    {
        // Returns true on successful patch, false otherwise
        internal static bool TryReplaceTexture2D(Sprite ogSprite)
        {
            if (ogSprite is not null && ogSprite.texture is not null)
            {
                if (/*!SpriteStore.changedList.Contains(ogSprite) && */TextureStore.textureDict.ContainsKey(ogSprite.texture.name))
                {
                    Graphics.CopyTexture(TextureStore.textureDict[ogSprite.texture.name], ogSprite.texture);
                    // SpriteStore.changedList.Add(ogSprite);
                    Log.LogDebug("OK! Replaced Texture " + ogSprite.texture.name + " for Sprite " + ogSprite.name);
                    return true;
                }
                else
                {
                    Log.LogDebug("FAIL! No Texture available for " + ogSprite.texture.name);
                }
            }
            return false;
        }

        internal static bool TryReplaceAudioClip(AudioClip audioClip)
        {
            Log.LogDebug($"[Music] Attempting to load custom music ({audioClip.name})...");

            if (AudioStore.audioDict.ContainsKey(audioClip.name))
            {
                AudioClip modClip = AudioStore.audioDict[audioClip.name];
                float[] samples = new float[modClip.samples * modClip.channels];
                modClip.GetData(samples, 0);
                audioClip.SetData(samples, 0);
                Log.LogDebug("[Music] Success. Applying custom music track!");
                return true;
            }
            else
            {
                Log.LogDebug($"[Music] Failed. Could not load the music track ({audioClip.name})");
                return false;
            }
        }
    }
}
