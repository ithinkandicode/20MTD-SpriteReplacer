using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

using flanne;

using AssetReplacer.AssetStore;

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

                if (AssetReplacer.ConfigTextureDynamicFogOfWar.Value)
                {
                    if (PlayerController.Instance is not null)
                    {
                        Transform fogOfWarCanvas = PlayerController.Instance.transform.Find("FogOfWarCanvas");
                        if (fogOfWarCanvas is not null)
                        {
                            Utils.ApplyDynamicFogOfWar(fogOfWarCanvas);
                        }
                        else
                        {
                            Debug.LogError("Unable to find fog of war canvas!");
                        }
                    }
                }
            }
        }
    }
}
