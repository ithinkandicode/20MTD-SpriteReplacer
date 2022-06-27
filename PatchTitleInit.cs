using HarmonyLib;
using UnityEngine;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer
{
    internal static class PatchTitleInit
    {
        //patches Textures when initializing the TitleScreen
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
            hPatchTitleInit.UnpatchSelf();
        }
    }
}
