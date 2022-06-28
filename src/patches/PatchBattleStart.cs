using HarmonyLib;
using UnityEngine;

namespace AssetReplacer.Patch
{
    internal static class PatchBattleStart
    {
        //patches Textures when initializing combat (ie. the "Battle" screen)
        [HarmonyPatch(typeof(flanne.Core.GameController), "Start")]
        [HarmonyPostfix]
        internal static void InitStatePostFix()
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
                AudioClip[] audioClips = Resources.FindObjectsOfTypeAll<AudioClip>();
                for (int i = 0; i < audioClips.Length; i++)
                {
                    Utils.TryReplaceAudioClip(audioClips[i]);
                }
            }
            // hPatchTitleInit.UnpatchSelf();
        }
    }
}
