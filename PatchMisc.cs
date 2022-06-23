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

            Log.LogDebug("found portrait: " + currPortrait.name);

            if (currPortrait != null)
            {
                Utils.ReplaceSpriteTexture(currPortrait);
            }
        }

        /*
        // Character Portrait
        [HarmonyPatch(typeof(CharacterDescription), "Refresh")]
        [HarmonyPrefix]
        static void RefreshPrefix(ref CharacterDescription __instance)
        {
            Log.LogDebug("!! Replacing texture for CharacterDescription");
            Log.LogDebug("!! Instance name=" + __instance.name);
            Log.LogDebug("!! Instance.GameObject name=" + __instance.gameObject.name);

            // Get private variable
            // Ref: https://api.raftmodding.com/client-code-examples/untitled
            Image currImg = Traverse.Create(__instance).Field("portrait").GetValue() as Image;

            //Log.LogDebug("!!# Image.name = " + currImg.name);
            //Log.LogDebug("!!# Image.sprite.name = " + currImg.sprite.name);

            //Utils.ReplaceSpriteTexture(currImg.sprite);            
        }
        */
    }
}
