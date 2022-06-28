using AssetReplacer.AssetStore;
using HarmonyLib;
using UnityEngine;

namespace AssetReplacer
{
    internal static class PatchTitleInit
    {
        //patches Textures when initializing the TitleScreen
        [HarmonyPatch(typeof(flanne.TitleScreen.TitleScreenController), "Start")]
        [HarmonyPostfix]
        internal static async void TitleScreenEnterPostfix()
        {
            if (AssetReplacer.ConfigEnableTextureMods.Value)
            {
                Sprite[] sprites = Resources.FindObjectsOfTypeAll<Sprite>();
                foreach (Sprite sprite in sprites)
                {
                    Utils.TryReplaceTexture2D(sprite);
                }
            }

            if (AssetReplacer.ConfigEnableMusicMods.Value)
            {
                await AudioStore.TaskInit;
                AudioClip[] audioClips = Resources.FindObjectsOfTypeAll<AudioClip>();
                foreach (AudioClip audioClip in audioClips)
                {
                    Utils.TryReplaceAudioClip(audioClip);
                }
            }

            // hPatchTitleInit.UnpatchSelf();
        }
    }
}
