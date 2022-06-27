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
        public static ConfigEntry<bool> configEnableTextureMods;
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

            // Config: General (args: section, key, default, description)
            configEnableTextureMods = Config.Bind<bool>("General", "EnableTextureMods", true, "Set to true to enable texture mods, false to completely disable them");
            configTextureModFolder = Config.Bind<string>("General", "TextureModFolder", "Vanilla", "Name of the active texture mod folder");
            configMusicModFolder = Config.Bind<string>("General", "MusicModFolder", "Demo", "Name of the active music mod folder");

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

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
