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
    }
}
