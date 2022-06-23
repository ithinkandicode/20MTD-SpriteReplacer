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
    /*
        Some objects are always pooled within the Awake function and therefore have to be accessed in its prefix. See console output for the log generated in Line #20 
        If the pooled object is not listed here that means it's pooled using ObjectPooler.AddObject and can be accessed by hooking that function.
        Other objects aren't pooled at all and expose their SpriteRenderer by themselves. Such as Pickups.ChestPickup, PlayerController and PlayerState.
    */
    static class PatchPools
    {
        [HarmonyPatch(typeof(ObjectPooler), "Awake")]
        [HarmonyPrefix]
        static bool AwakePrefix(ref ObjectPooler __instance)
        {

            foreach (ObjectPoolItem objectPoolItem in __instance.itemsToPool)
            {
                GameObject GO = objectPoolItem.objectToPool;

                Log.LogDebug("[Pools>Awake] Item to pool (name|tag1|amount|shouldExpand|tag2): " + GO.name + "|" + objectPoolItem.tag + "|" + objectPoolItem.amountToPool + "|" + objectPoolItem.shouldExpand + "|" + objectPoolItem.objectToPool.tag);

                SpriteRenderer currSpriteRenderer = GO.GetComponent<SpriteRenderer>();

                if (currSpriteRenderer != null)
                {
                    if (currSpriteRenderer.sprite != null)
                    {
                        Utils.ReplaceSpriteTexture(currSpriteRenderer.sprite);
                    }
                }
            }
            return true;
        }


        [HarmonyPatch(typeof(ObjectPooler), "AddObject")]
        [HarmonyPrefix]
        static bool AddObjectPrefix(string tag, GameObject GO, int amt = 3, bool exp = true)
        {
            Log.LogDebug("[Pools>AddObject] Item to pool (name|tag|amount|shouldExpand): " + GO.name + "|" + tag + "|" + amt + "|" + exp);

            SpriteRenderer currSpriteRenderer = GO.GetComponent<SpriteRenderer>();

            if (currSpriteRenderer != null)
            {
                if (currSpriteRenderer.sprite != null)
                {
                    Utils.ReplaceSpriteTexture(currSpriteRenderer.sprite);
                }
            }

            return true;
        }
    }
}