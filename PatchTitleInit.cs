using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    internal static class PatchTitleInit
    {
        //patches Textures when initializing the TitleScreen
        [HarmonyPatch(typeof(flanne.TitleScreen.InitState), "Enter")]
        [HarmonyPostfix]
        internal static void TitleScreenEnterPostfix()
        {
            Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            foreach (Sprite sprite in sprites)
            {
                SpriteStore.CleanList();
                if (!SpriteStore.ChangedSprites.Contains(sprite))
                {
                    bool result = Utils.ReplaceSpriteTexture(sprite);
                }
            }

            hPatchTitleInit.UnpatchSelf();
        }

        /**
         * Cursor Sprite Patch
         * 
         * Required because the cursor is initially set as a hardware cursor in
         * Unity's player settings (so it's not accessible via FindObjectsOfTypeAll).
         * The hardware cursor sprite is T_Cursor. 
         * 
         * On the Battle screen, the gamepad cursor is used instead (which uses
         * the sprite T_CursorSprite). The gamepad cursor is also used if a
         * gamepad is active (see GamepadCursor.cs, in particular look for
         * "SetActive" and "Cursor.visible").
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

                // Check if the gamepad cursor is already being used; if so, we don't need to replace it
                GameObject gamepadCursor = GameObject.Find("GamepadCursor"); // (only finds active objects)

                if (gamepadCursor == null)
                {
                    Cursor.SetCursor(cursorImg.sprite.texture, CursorMode.Auto);
                }
            }
        }
    }
}