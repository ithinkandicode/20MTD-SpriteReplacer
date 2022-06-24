using System.IO;
using UnityEngine;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    internal class Utils
    {
        // Returns true on successful patch, false otherwise
        internal static bool ReplaceSpriteTexture(Sprite targetSprite)
        {
            if (targetSprite != null)
            {
                //Log.LogDebug("Sprite.name:" + targetSprite.name);

                Texture2D spriteTexture = targetSprite.texture;

                if (spriteTexture != null)
                {
                    string path = Path.Combine(SourceDirectory, SpriteInfo.GetFilePath(spriteTexture.name));

                    //Log.LogDebug("Sprite.Texture.name:" + spriteTexture.name);
                    //Log.LogDebug("SearchPath:" + path);

                    if (File.Exists(path))
                    {
                        Sprite ogSprite = targetSprite;
                        ogSprite.texture.LoadImage(File.ReadAllBytes(path));
                        Vector2 standardisedPivot = new Vector2(ogSprite.pivot.x / ogSprite.rect.width, ogSprite.pivot.y / ogSprite.rect.height);
                        Sprite sprite = Sprite.Create(ogSprite.texture, ogSprite.rect, standardisedPivot, ogSprite.pixelsPerUnit);
                        sprite.name = ogSprite.name;
                        spriteTexture = sprite.texture;

                        Log.LogDebug("OK! Replaced: " + path);

                        SpriteStore.ChangedSprites.Add(targetSprite);

                        return true;
                    }
                    else
                    {
                        Log.LogDebug("FAIL! No Texture available for " + spriteTexture.name);
                    }
                }
            }

            return false;
        }
    }
}
