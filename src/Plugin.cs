﻿using System;
using BepInEx;
using HarmonyLib;
using BepInEx.Logging;
using BepInEx.Configuration;
using AssetReplacer.AssetStore;

namespace AssetReplacer
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class AssetReplacer : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        // Register config settings
        public static ConfigEntry<bool> ConfigEnableAllMods;
        public static ConfigEntry<bool> ConfigEnableTextureMods;
        public static ConfigEntry<bool> ConfigEnableMusicMods;
        public static ConfigEntry<string> ConfigTextureModFolders;
        public static ConfigEntry<string> ConfigMusicModFolders;
        public static ConfigEntry<bool> ConfigTextureDynamicFogOfWar;

        internal static Harmony hPatchTitleCursor;
        internal static Harmony hPatchTitleStart;
        internal static Harmony hPatchBattleStart;
        internal static Harmony hPatchAudioSource;

        private void Awake()
        {
            Log = base.Logger;

            // Configs (args: section, key, default, description)
            ConfigEnableAllMods = Config.Bind<bool>("All", "EnableAll", true, "Set to true to enable all mods, false to completely disable them");
            ConfigEnableTextureMods = Config.Bind<bool>("Textures", "EnableTextureMods", true, "Set to true to enable texture mods, false to completely disable them");
            ConfigEnableMusicMods = Config.Bind<bool>("Music", "EnableMusicMods", true, "Set to true to enable music mods, false to completely disable them");
            ConfigMusicModFolders = Config.Bind<string>("Music", "MusicModFolders", "Demo",
            @"Name of the active music mod folder(s)
            For loading multiple texture mods add them comma seperated in ascending priority
            Example:
                MusicMod1,MusicMod2,MusicMod3
            MusicMod3 overwrites the soundfiles of MusicMod2 which overwrites the soundfiles of MusicMod1");
            // Config: General (args: section, key, default, description)
            ConfigEnableTextureMods = Config.Bind<bool>("General", "EnableTextureMods", true, "Set to true to enable texture mods, false to completely disable them");
            ConfigTextureModFolders = Config.Bind<string>("General", "TextureModFolders", "Demo",
            @"Name of the active texture mod folder(s)
            For loading multiple texture mods add them comma seperated in ascending priority
            Example:
                TextureMod1,TextureMod2,TextureMod3
            TextureMod3 overwrites the textures of TextureMod2 which overwrites the textures of TextureMod1");

            ConfigTextureDynamicFogOfWar = Config.Bind<bool>(
                "Textures",
                "EnableDynamicFogOfWarColor",
                true,
                "Set to true to dynamically change the fog of war color based on the ground tile texture, false to disable.");

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            bool enableAllMods = ConfigEnableAllMods.Value;

            if (!enableAllMods)
            {
                Log.LogInfo("All mods are disabled in the config (BepInEx\\config\\AssetReplacer.cfg). No game assets will be replaced");
                return;
            }

            this.patchAssets();
        }

        private void patchAssets()
        {
            //Textures
            if (ConfigEnableTextureMods.Value)
            {
                foreach (string modFolder in ConfigTextureModFolders.Value.Split(','))
                {
                    Log.LogInfo("Configured Texture Mod Directory: " + modFolder);
                    FileLoader.TextureModFolders.Add(modFolder);
                }
                TextureStore.Init();
            }
            else
            {
                Log.LogInfo("Texture mods are disabled in the config (BepInEx\\config\\AssetReplacer.cfg). Default game textures will be used");
            }

            //Music
            if (ConfigEnableMusicMods.Value)
            {
                foreach (string modFolder in ConfigMusicModFolders.Value.Split(','))
                {
                    Log.LogInfo("Configured Music Mod Directory: " + modFolder);
                    FileLoader.MusicModFolders.Add(modFolder);
                }
                AudioStore.Init();

            }
            else
            {
                Log.LogInfo("Music mods are disabled in the config (BepInEx\\config\\AssetReplacer.cfg). Default game music will be used");
            }

            try
            {
                hPatchTitleCursor = Harmony.CreateAndPatchAll(typeof(Patch.PatchTitleCursor), "PatchTitleCursor");
                hPatchTitleStart = Harmony.CreateAndPatchAll(typeof(Patch.PatchTitleStart), "PatchTitleStart");
                hPatchBattleStart = Harmony.CreateAndPatchAll(typeof(Patch.PatchBattleStart), "PatchBattleStart");
                hPatchAudioSource = Harmony.CreateAndPatchAll(typeof(Patch.PatchAudioSource), "PatchAudioSource");
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods.");
            }
        }
    }
}
