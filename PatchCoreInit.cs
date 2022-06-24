using HarmonyLib;
using UnityEngine;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    internal static class PatchCoreInit
    {
        //patches Textures when initializing combat
        [HarmonyPatch(typeof(flanne.Core.InitState), "Enter")]
        [HarmonyPostfix]
        internal static void InitStatePostFix()
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
            hPatchCoreInit.UnpatchSelf();
        }
    }
}

