using flanne.Core;
using HarmonyLib;
using UnityEngine;

namespace SpriteReplacer
{
    static class PatchStates
    {
        //patches Textures when entereing the TitleScreen
        [HarmonyPatch(typeof(flanne.TitleScreen.InitState), "Enter")]
        [HarmonyPostfix]
        static void TitleScreenEnterPostfix()
        {
            Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            foreach (Sprite sprite in sprites)
            {
                bool result = Utils.ReplaceSpriteTexture(sprite);
            }
        }

        //patches Textures when starting combat
        [HarmonyPatch(typeof(CombatState), "Enter")]
        [HarmonyPostfix]
        static void CombatStateEnterPostfix()
        {
            Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();
            foreach (Sprite sprite in sprites)
            {
                bool result = Utils.ReplaceSpriteTexture(sprite);
            }
        }
    }
}