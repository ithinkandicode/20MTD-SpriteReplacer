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

            //Console.WriteLine("@@@|found portrait: " + currPortrait.name + "|@@@");


            Utils.ReplaceSpriteTexture(currPortrait);
        }

        /*
        // Character Portrait
        [HarmonyPatch(typeof(CharacterDescription), "Refresh")]
        [HarmonyPrefix]
        static void RefreshPrefix(ref CharacterDescription __instance)
        {
            Console.WriteLine("!! Replacing texture for CharacterDescription");
            Console.WriteLine("!! Instance name=" + __instance.name);
            Console.WriteLine("!! Instance.GameObject name=" + __instance.gameObject.name);

            // Get private variable
            // Ref: https://api.raftmodding.com/client-code-examples/untitled
            Image currImg = Traverse.Create(__instance).Field("portrait").GetValue() as Image;

            //Console.WriteLine("!!# Image.name = " + currImg.name);
            //Console.WriteLine("!!# Image.sprite.name = " + currImg.sprite.name);

            //Utils.ReplaceSpriteTexture(currImg.sprite);            
        }
        */
    }
}
