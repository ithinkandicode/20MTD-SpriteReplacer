using HarmonyLib;
using UnityEngine;

namespace AssetReplacer.Patch
{
    internal static class SceneLoadPatches
    {
        //patches Textures when initializing the TitleScreen
        [HarmonyPatch(typeof(flanne.TitleScreen.TitleScreenController), "Start")]
        //patches Textures when initializing combat (ie. the "Battle" screen)
        [HarmonyPatch(typeof(flanne.ObjectPooler), "Awake")]
        [HarmonyPrefix]
        internal static void StartPrefix()
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
