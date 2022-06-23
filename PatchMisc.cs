using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using flanne;
using flanne.UI;
using flanne.Core;
using flanne.TitleScreen;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    class PatchMisc
    {
        // Character Portrait
        [HarmonyPatch(typeof(CharacterEntry), "Refresh")]
        [HarmonyPrefix]
        static void RefreshPrefix(ref CharacterDescription __instance)
        {
            // Looks like we can access anything from here: CharacterData.cs
            // Note: This should mean we can change names and descriptions too
            Sprite currPortrait = __instance.data.portrait;

            Log.LogDebug("[Misc] Portrait: " + currPortrait.name);

            if (currPortrait != null)
            {
                Utils.ReplaceSpriteTexture(currPortrait);
            }
        }

        // Main Scene Controller: TitleScreen
        // This object has lots of properties that we can target
        // Note: We could target the music too (AudioClip titleScreenMusic)
        [HarmonyPatch(typeof(TitleScreenController), "Start")]
        [HarmonyPostfix]
        static void TitleScreenStartPostfix(ref TitleScreenController __instance)
        {
            // There's only ever 1 canvas. It holds most GUI objects
            //GameObject canvas = GameObject.Find("Canvas");

            GameObject cursor = __instance.gamepadCursor;
            Animator eyes = __instance.eyes;

            Image cursorImg = cursor.GetComponentInChildren<Image>();
            Image eyesImg = eyes.GetComponentInChildren<Image>();

            if (cursorImg != null)
            {
                Utils.ReplaceSpriteTexture(cursorImg.sprite);

                // Cursor Notes: Initially only applies to the gamepad/in-game cursor (T_CursorSprite).
                // The hardware cursor image (T_Cursor) is separate and is set in Unity's settings,
                // so we have to overide it here (but only if the gamepad cursor isn't active, since
                // that uses the correct T_CursorSprite texture).
                GameObject gamepadCursor = GameObject.Find("GamepadCursor");

                if (gamepadCursor == null)
                {
                    Cursor.SetCursor(cursorImg.sprite.texture, CursorMode.Auto);
                }
            }

            if (eyesImg != null)
            {
                Utils.ReplaceSpriteTexture(eyesImg.sprite);
            }           
        }


        // Controller: Battle
        [HarmonyPatch(typeof(GameController), "Start")]
        [HarmonyPostfix]
        static void BattleStartPostfix(ref GameController __instance)
        {
            //...
        }

        // Controller: Player
        [HarmonyPatch(typeof(PlayerController), "Start")]
        [HarmonyPostfix]
        static void PlayerStartPostfix(ref PlayerController __instance)
        {
            //...
        }
    }
}
