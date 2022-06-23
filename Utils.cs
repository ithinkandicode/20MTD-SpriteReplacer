using System;
using System.IO;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using flanne.UI;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    public class Utils
    {
        // Returns true on successful patch, false otherwise
        static public bool ReplaceSpriteTexture(Sprite targetSprite)
        {
            if (targetSprite != null)
            {
                //Log.LogDebug("Sprite.name:" + targetSprite.name);

                Texture2D spriteTexture = targetSprite.texture;

                if (spriteTexture != null)
                {
                    string subfolder = Utils.GetTextureSubfolder(spriteTexture.name);
                    string modPath = SpriteReplacer.configTextureModFolder.Value;
                    string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Textures", modPath, subfolder, spriteTexture.name + ".png");

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

                        Log.LogInfo("OK! Replaced: " + path);

                        return true;
                    }
                    else
                    {
                        Log.LogInfo("FAIL! No image at: " + path);
                    }
                }
            }

            return false;
        }

        // Returns the subfolder to the sprite (if available), otherwise returns an empty string
        // Note: Yes this is innefficient, but the game only has ~100 sprites ATOW
        static public string GetTextureSubfolder(string spriteName)
        {
            return SpriteInfo.GetSubFolder(spriteName);
        }
    }
}
