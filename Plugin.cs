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
        public static ConfigEntry<string> configTextureModFolder;

        private void Awake()
        {
            Log = base.Logger;

            // Setup config (args: section, key, default, description)
            configTextureModFolder = Config.Bind<string>("General", "TextureModFolder", "Vanilla", "Name of the active texture mod folder");

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            string modPath = configTextureModFolder.Value;
            string subfolder = "";
            string currentModPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Textures", modPath, subfolder);

            Log.LogInfo("Curent texture mods path: " + currentModPath);

            // Debug options, in case you want to test specific patchers
            bool doPanels = true; // GUI, and everything in the menus before a run (incl. character sprites, weapons, rune icons)
            bool doPools = true; // Loads of sprites, incl. enemies and pickups
            bool doMisc = true; // Misc things that aren't covered bythe above

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
                    // Poolhandler: Most things in the battle (ie. game) screen
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
                    // MiscHandler: Everything else, special cases
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
