using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using System;
using System.Collections;
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
        public static ConfigEntry<string> configEnableTextureMods;
        public static ConfigEntry<string> configTextureModFolder;
        public static ConfigEntry<string> configDebugDoPanels;
        public static ConfigEntry<string> configDebugDoPools;
        public static ConfigEntry<string> configDebugDoMisc;

        private void Awake()
        {
            Log = base.Logger;

            // Config: General (args: section, key, default, description)
            configEnableTextureMods = Config.Bind<string>("General", "EnableTextureMods", "True", "True to enable texture mods, False to completely disable them");
            configTextureModFolder  = Config.Bind<string>("General", "TextureModFolder", "Vanilla", "Name of the active texture mod folder");

            // Config: Debug (in case you want to test specific patchers)
            configDebugDoPanels = Config.Bind<string>("Debug", "DebugPatchPanels", "True", "Debug option. Set to False to skip patching panels (GUI, and everything in the menus before a run, including character sprites, weapons, and rune icons)");
            configDebugDoPools  = Config.Bind<string>("Debug", "DebugPatchPools",  "True", "Debug option. Set to False to skip patching pools (most enemies and pickups)");
            configDebugDoMisc   = Config.Bind<string>("Debug", "DebugPatchMisc",   "True", "Debug option. Set to False to skip patching misc (special cases that aren't covered by panels/pools)");

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            bool enableMods = bool.Parse(configEnableTextureMods.Value);

            if (!enableMods)
            {
                Log.LogInfo("Texture mods are disabled in the config (BepInEx\\config\\SpriteReplacer.cfg). Default game textures will be used");
                return;
            }

            string modPath = configTextureModFolder.Value;
            string subfolder = "";
            string currentModPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Textures", modPath, subfolder);

            Log.LogInfo("Current textures folder: " + configTextureModFolder.Value);
            Log.LogInfo("Current textures path: " + currentModPath);

            bool doPanels = bool.Parse(configDebugDoPanels.Value);
            bool doPools = bool.Parse(configDebugDoPools.Value);
            bool doMisc = bool.Parse(configDebugDoMisc.Value);

            if (doPanels)
            {
                try
                {
                    // Panels: Any GUI element
                    // Includes basically everything on the title/loadout screen, except the blinking eyes and character portraits
                    Harmony.CreateAndPatchAll(typeof(PatchPanels));
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods (PanelHandler).");
                }
            }

            if (doPools)
            {
                try
                {
                    // Pools: Most things in the battle (ie. game) screen
                    Harmony.CreateAndPatchAll(typeof(PatchPools));
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods (PoolHandler).");
                }
            }

            if (doMisc)
            {
                try
                {
                    // Misc: Everything else, special cases
                    Harmony.CreateAndPatchAll(typeof(PatchMisc));
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch methods (MiscHandler).");
                }
            }
        }
    }
}
