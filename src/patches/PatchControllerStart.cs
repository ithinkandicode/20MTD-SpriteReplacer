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

                if(AssetReplacer.ConfigTextureDynamicFogOfWar.Value)
                {
                    Transform fogOfWarCanvas = PlayerController.Instance.transform.Find("FogOfWarCanvas");

                    if(fogOfWarCanvas != null)
                    {
                        Transform child = fogOfWarCanvas.GetChild(0);

                        if(child == null)
                        {
                            Debug.LogError("Fog of war canvas has no child!");
                            return;
                        }

                        RawImage img = child.GetComponent<RawImage>();

                        if(img == null)
                        {
                            Debug.LogError("Fog of war image has no RawImage component!");
                            return;
                        }

                        Texture2D grasstile = null;
                        if(!TextureStore.textureDict.TryGetValue("T_TileGrass", out grasstile))
                            return;

                        Color[] colors = grasstile.GetPixels();

                        Color additive = Color.black;

                        foreach(Color color in colors)
                        {
                            additive += color;
                        }

                        Color average = additive / colors.Length * 0.75f;

                        Material material = img.material;
                        material.color = average;
                    }
                    else
                        Debug.LogError("Unable to find fog of war canvas!");
                }
            }
        }
    }
}
