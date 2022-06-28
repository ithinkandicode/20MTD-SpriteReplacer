using AssetReplacer.AssetStore;
using HarmonyLib;
using UnityEngine;

namespace AssetReplacer.Patch
{
    internal static class PatchTitleStart
    {
        //patches Textures when initializing the TitleScreen
        [HarmonyPatch(typeof(flanne.TitleScreen.TitleScreenController), "Start")]
        [HarmonyPostfix]
        internal static async void StartPostfix()
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
