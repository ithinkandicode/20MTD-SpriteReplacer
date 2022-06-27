using UnityEngine;
using HarmonyLib;
using flanne;
using flanne.TitleScreen;
using static SpriteReplacer.SpriteReplacer;
using static SpriteReplacer.UtilsMusic;

namespace SpriteReplacer
{
    internal class MusicPatchTitle
    {
        internal static string musicfilename = "title.mp3";

        [HarmonyPatch(typeof(TitleScreenController), "Start")]
        [HarmonyPostfix]
        internal static void TitleScreenControllerStartPostfix(ref TitleScreenController __instance)
        {
            initMusicPatch(__instance);
        }

        // Async due to waiting for the music file to load
        internal static async void initMusicPatch(TitleScreenController controllerInstance)
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
            controllerInstance.titleScreenMusic = customAudioClip;

            AudioManager.Instance.FadeOutMusic(1f);
            AudioManager.Instance.PlayMusic(controllerInstance.titleScreenMusic);
            //AudioManager.Instance.PlayMusic(modMusicClip); // also works
            AudioManager.Instance.FadeInMusic(1f);
        }
    }
}
