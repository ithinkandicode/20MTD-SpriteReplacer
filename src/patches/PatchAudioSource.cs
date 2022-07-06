using flanne;
using AssetReplacer.AssetStore;
using HarmonyLib;
using UnityEngine;
using static AssetReplacer.AssetReplacer;

namespace AssetReplacer.Patch
{
    internal static class PatchAudioSource
    {
        [HarmonyPatch(typeof(AudioSource), "Play", new System.Type[] { })]
        [HarmonyPrefix]
        static void AudioSourcePlayPrefix(AudioSource __instance)
        {
            Utils.TryReplaceAudioClip(__instance.clip);
        }

        [HarmonyPatch(typeof(AudioSource), "Play", new System.Type[] { typeof(float) })]
        [HarmonyPrefix]
        static void AudioSourcePlayDelayPrefix(AudioSource __instance, float delay)
        {
            Utils.TryReplaceAudioClip(__instance.clip);
        }

        [HarmonyPatch(typeof(AudioManager), "PlayMusic")]
        [HarmonyPrefix]
        static async void AwakePrefix(AudioManager __instance, AudioClip clip)
        {
            await AudioStore.TaskInit; //wait for custom music to be fully loaded
            AudioSource audioSource = (AudioSource)Traverse.Create(__instance).Field("musicSource").GetValue();
            audioSource.Play();
        }
    }
}