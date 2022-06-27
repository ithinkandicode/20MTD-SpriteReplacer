using UnityEngine;
using SpriteReplacer.AssetStore;
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
                    Log.LogInfo("OK! Replaced Texture " + ogSprite.texture.name + " for Sprite " + ogSprite.name);
                    return true;
                }
                else
                {
                    Log.LogInfo("FAIL! No Texture available for " + ogSprite.texture.name);
                }
            }
            return false;
        }
    }
}
