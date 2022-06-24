using flanne.TitleScreen;
using HarmonyLib;
using UnityEngine;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    static class PatchInitState
    {
        [HarmonyPatch(typeof(InitState), "Enter")]
        [HarmonyPostfix]
        static void EnterPostfix()
        {
            Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            foreach (Sprite sprite in sprites)
            {
                bool result = Utils.ReplaceSpriteTexture(sprite);
            }
        }
    }
}