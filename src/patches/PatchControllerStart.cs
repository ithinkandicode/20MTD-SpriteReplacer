using HarmonyLib;
using UnityEngine;

namespace AssetReplacer.Patch
{
    internal static class PatchControllerStart
    {
        //patches Textures when initializing the TitleScreen
        [HarmonyPatch(typeof(flanne.TitleScreen.TitleScreenController), "Start")]
        //patches Textures when initializing combat (ie. the "Battle" screen)
        [HarmonyPatch(typeof(flanne.Core.GameController), "Start")]
        [HarmonyPostfix]
        internal static void StartPostfix()
        {
            if (AssetReplacer.ConfigEnableTextureMods.Value)
            {
                Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();
                foreach (Sprite sprite in sprites)
                {
                    Utils.TryReplaceTexture2D(sprite);
                }
            }

            if (AssetReplacer.ConfigEnableSpriteAnimationMods.Value)
            {
                SpriteRenderer[] spriteRenderers = Resources.FindObjectsOfTypeAll<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    Utils.TryAnimateSpriteRenderer(spriteRenderer);
                }
            }
        }
    }
}
