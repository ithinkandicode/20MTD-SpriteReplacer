using UnityEngine;
using HarmonyLib;
using flanne;
using flanne.Core;
using static AssetReplacer.AssetReplacer;
using static AssetReplacer.UtilsMusic;

namespace AssetReplacer
{
    internal class MusicPatchBattle
    {
        internal static string musicfilename = "battle.mp3";

        [HarmonyPatch(typeof(GameController), "Start")]
        [HarmonyPostfix]
        internal static void TitleScreenControllerStartPostfix(ref GameController __instance)
        {
            initMusicPatch(__instance);
        }

        // Async due to waiting for the music file to load
        internal static async void initMusicPatch(GameController controllerInstance)
        {
            Log.LogDebug($"[Music] Attempting to load custom music ({musicfilename})...");

            AudioClip customAudioClip = await LoadMusicFromDisk(musicfilename, AudioType.MPEG);

            if (customAudioClip == null)
            {
                Log.LogError($"[Music] Failed. Could not load the music track ({musicfilename})");
                return;
            }

            Log.LogDebug("[Music] Success. Applying custom music track!");

            // Update the music.
            // This won't have an immediate effect, we need to stop the
            // original music from playing first
            controllerInstance.battleMusic = customAudioClip;

            AudioManager.Instance.FadeOutMusic(1f);
            AudioManager.Instance.PlayMusic(controllerInstance.battleMusic);
            //AudioManager.Instance.PlayMusic(modMusicClip); // also works
            AudioManager.Instance.FadeInMusic(1f);
        }
    }
}
