using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer
{
    internal static class PatchTitleCursor
    {
        /**
         * Cursor Sprite Patch
         * 
         * Updating the cursor has to be done separately from other objects,
         * because the cursor is initially set as a hardware cursor (in Unity
         * via Edit > Project Settings > Player). This means it's not accessible
         * by the FindObjectsOfTypeAll in PatchTitleInit.
         * 
         * Hardware cursor sprite = T_Cursor
         * Gamepad  cursor sprite = T_CursorSprite
         * 
         * On the Battle screen, the gamepad cursor is always used.
         * It's only used on the title screen if a gamepad is active.
         * See GamepadCursor.cs: "SetActive" & "Cursor.visible"
         */
        [HarmonyPatch(typeof(flanne.TitleScreen.TitleScreenController), "Start")]
        [HarmonyPrefix]
        static void TitleScreenCursorStartPrefix(ref flanne.TitleScreen.TitleScreenController __instance)
        {
            GameObject cursor = __instance.gamepadCursor;
            Image cursorImg = cursor.GetComponentInChildren<Image>();

            if (cursorImg != null)
            {
                // Manual replacement (as this method runs before TitleScreenEnterPostfix).
                // This is just an easy way to get the updated sprite
                Utils.ReplaceSpriteTexture(cursorImg.sprite);

                Vector2 hotspot = new Vector2(8, 8); // set in Unity via Player > Cursor Hotspot
                Cursor.SetCursor(cursorImg.sprite.texture, hotspot, CursorMode.Auto);
            }

            // Remove this patch, as the hardware cursor image only needs to be updated once
            hPatchTitleCursor.UnpatchSelf();
        }
    }
}
