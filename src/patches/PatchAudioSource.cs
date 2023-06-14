using flanne;
using AssetReplacer.AssetStore;
using HarmonyLib;
using UnityEngine;

namespace AssetReplacer.Patch
{
    internal static class PatchAudioSource
    {
        [HarmonyPatch(typeof(AudioSource), "Play", new System.Type[] { })]
        [HarmonyPrefix]
        static void AudioSourcePlayPrefix(AudioSource __instance)
        {
            if (__instance.clip is not null && AudioStore.audioDict.ContainsKey(__instance.clip.name))
            {
                __instance.clip = AudioStore.audioDict[__instance.clip.name];
            }
        }

        [HarmonyPatch(typeof(AudioSource), "Play", new System.Type[] { typeof(float) })]
        [HarmonyPrefix]
        static void AudioSourcePlayDelayPrefix(AudioSource __instance, float delay)
        {
            if (__instance.clip is not null && AudioStore.audioDict.ContainsKey(__instance.clip.name))
            {
                __instance.clip = AudioStore.audioDict[__instance.clip.name];
            }
        }

        [HarmonyPatch(typeof(AudioManager), "PlayMusic")]
        [HarmonyPrefix]
        static async void AwakePrefix(AudioManager __instance, AudioClip clip)
        {
            await AudioStore.TaskInit; //wait for custom music to be fully loaded
            AudioSource audioSource = (AudioSource)Traverse.Create(__instance).Field("musicSource").GetValue();
            if (audioSource is not null)
            {
                audioSource.Play();
            }
        }
    }
}