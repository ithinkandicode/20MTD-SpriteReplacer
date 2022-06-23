using System;
using System.IO;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using flanne.UI;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    class PatchPanels
    {
        [HarmonyPatch(typeof(Panel), "Start")]
        [HarmonyPrefix]
        static void StartPrefix(ref Panel __instance)
        {
            Log.LogDebug("[Panel] START of panel loop...");
            CanvasGroup[] canvasComponentsInChildren = __instance.GetComponentsInChildren<CanvasGroup>();

            for (int canvasIndex = 0; canvasIndex < canvasComponentsInChildren.Length; canvasIndex++)
            {
                GameObject currObject = canvasComponentsInChildren[canvasIndex].gameObject;
                Log.LogDebug("[Panel] Panel name ("+ canvasIndex.ToString() +"): " + currObject.name);
                int panelIndex = 0;

                foreach (Image currImg in currObject.GetComponentsInChildren<Image>())
                {
                    Log.LogDebug("[Panel] Panel image (" + panelIndex.ToString() + "): " + currImg.name);
                    Utils.ReplaceSpriteTexture(currImg.sprite);
                    panelIndex++;
                }
            }

            Log.LogDebug("[Panel] END of panel loop");
        }
    }
}
