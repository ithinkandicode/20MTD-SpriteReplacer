using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using System;
using System.IO;
using UnityEngine;
using HarmonyLib;

namespace SpriteReplacer
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class SpriteReplacer : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        // Register config settings
        public static ConfigEntry<bool> configEnableAllMods;
        public static ConfigEntry<bool> configEnableTextureMods;
        public static ConfigEntry<bool> configEnableMusicMods;
        public static ConfigEntry<string> configTextureModFolder;
        public static ConfigEntry<string> configMusicModFolder;
        public static string SourceSpritesDirectory;
        public static string SourceMusicDirectory;
        internal static Harmony hPatchTitleCursor;
        internal static Harmony hPatchTitleInit;
        internal static Harmony hPatchCoreInit;

        private void Awake()
        {
            Log = base.Logger;

            // Configs (args: section, key, default, description)
            configEnableAllMods = Config.Bind<bool>("All", "EnableAll", true, "Set to true to enable all mods, false to completely disable them");
            configEnableTextureMods = Config.Bind<bool>("Textures", "EnableTextureMods", true, "Set to true to enable texture mods, false to completely disable them");
            configTextureModFolder = Config.Bind<string>("Textures", "TextureModFolder", "Vanilla", "Name of the active texture mod folder");
            configEnableMusicMods = Config.Bind<bool>("Music", "EnableMusicMods", true, "Set to true to enable music mods, false to completely disable them");
            configMusicModFolder = Config.Bind<string>("Music", "MusicModFolder", "Demo", "Name of the active music mod folder");

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            bool enableAllMods = configEnableAllMods.Value;

            if (!enableAllMods)
            {
                Log.LogInfo("All mods are disabled in the config (BepInEx\\config\\SpriteReplacer.cfg). No game assets will be replaced");
                return;
            }

            // Setup all directory vars
            SourceSpritesDirectory = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Textures", configTextureModFolder.Value);
            SourceMusicDirectory = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Music", configMusicModFolder.Value);

            this.patchSprites();
            this.patchMusic();
        }

        private void patchSprites()
        {
            bool enableMods = configEnableTextureMods.Value;

            if (!enableMods)
            {
                Log.LogInfo("Texture mods are disabled in the config (BepInEx\\config\\SpriteReplacer.cfg). Default game textures will be used");
                return;
            }

            Log.LogInfo("Current textures folder: " + configTextureModFolder.Value);
            Log.LogInfo("Current textures path: " + SourceSpritesDirectory);

            SpriteInfo.Init();

            try
            {
                // All these only need to run once, so we're saving them as static vars, so we can unpatch them later
                hPatchTitleCursor = Harmony.CreateAndPatchAll(typeof(PatchTitleCursor), "PatchTitleCursor");
                hPatchTitleInit = Harmony.CreateAndPatchAll(typeof(PatchTitleInit), "PatchTitleInit");
                hPatchCoreInit = Harmony.CreateAndPatchAll(typeof(PatchCoreInit), "PatchCoreInit");
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch texture methods.");
            }
        }

        private void patchMusic()
        {
            bool enableMods = configEnableMusicMods.Value;

            if (!enableMods)
            {
                Log.LogInfo("Music mods are disabled in the config (BepInEx\\config\\SpriteReplacer.cfg). Default game music will be used");
                return;
            }

            try
            {
                Harmony.CreateAndPatchAll(typeof(MusicPatchTitle), "MusicPatch");
                Harmony.CreateAndPatchAll(typeof(MusicPatchBattle), "MusicPatchBattle");
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch music methods.");
            }
        }
    }
}
