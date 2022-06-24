using flanne.Core;
using HarmonyLib;
using UnityEngine;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    internal static class PatchStates
    {
        //patches Textures when entereing the TitleScreen
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
        }

        //patches Textures when starting combat
        [HarmonyPatch(typeof(CombatState), "Enter")]
        [HarmonyPostfix]
        internal static void CombatStateEnterPostfix()
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
        }
    }
}