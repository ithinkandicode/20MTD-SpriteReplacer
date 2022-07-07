using System;
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
        public static ConfigEntry<string> ConfigTextureModFolders;
        public static ConfigEntry<bool> ConfigEnableAudioMods;
        public static ConfigEntry<string> ConfigAudioModFolders;
        public static ConfigEntry<bool> ConfigEnableAssetBundleMods;
        public static ConfigEntry<string> ConfigAssetBundleModFolders;
        public static ConfigEntry<bool> ConfigEnableSpriteAnimationMods;
        public static ConfigEntry<string> ConfigSpriteAnimationModFolders;
        public static ConfigEntry<string> ConfigFolderStructure;
        internal static Harmony hPatchTitleCursor;
        internal static Harmony hSceneLoadPatches;
        internal static Harmony hPatchAudioSource;

        private void Awake()
        {
            Log = base.Logger;

            // Configs (args: section, key, default, description)
            ConfigEnableAllMods = Config.Bind<bool>("All", "EnableAll", true, "Set to true to enable all mods, false to completely disable them");
            ConfigFolderStructure = Config.Bind<string>("All", "FolderStructure", "Thunderstore", new ConfigDescription("Change expected folder structure for legacy mods.", new AcceptableValueList<string>(new string[] { "Thunderstore", "Legacy" })));
            ConfigEnableTextureMods = Config.Bind<bool>("Textures", "EnableTextureMods", true, "Set to true to enable texture mods, false to completely disable them");
            ConfigTextureModFolders = Config.Bind<string>("Textures", "TextureModFolders", "Demo",
                "Name of the active texture mod folder(s)" + System.Environment.NewLine +
                "For loading multiple texture mods add them comma seperated in ascending priority" + System.Environment.NewLine +
                "Example:" + System.Environment.NewLine +
                "   \"TextureMod1,TextureMod2,TextureMod3\"" + System.Environment.NewLine +
                "TextureMod3 overwrites the textures of TextureMod2 which overwrites the textures of TextureMod1");
            ConfigEnableAudioMods = Config.Bind<bool>("Audio", "EnableAudioMods", true, "Set to true to enable audio mods, false to completely disable them");
            ConfigAudioModFolders = Config.Bind<string>("Audio", "AudioModFolders", "Demo",
                "Name of the active audio mod folder(s)" + System.Environment.NewLine +
                "For loading multiple audio mods add them comma seperated in ascending priority" + System.Environment.NewLine +
                "Example:" + System.Environment.NewLine +
                "   \"AudioMod1,AudioMod2,AudioMod3\"" + System.Environment.NewLine +
                "AudioMod3 overwrites the soundfiles of AudioMod2 which overwrites the soundfiles of AudioMod1");
            ConfigEnableAssetBundleMods = Config.Bind<bool>("AssetBundle", "EnableAssetBundleMods", true, "Set to true to enable AssetBundle mods, false to completely disable them");
            ConfigAssetBundleModFolders = Config.Bind<string>("AssetBundle", "AssetBundleModFolders", "Demo",
                "Name of the active AssetBundle mod folder(s)" + System.Environment.NewLine +
                "For loading multiple AssetBundle mods add them comma seperated in ascending priority" + System.Environment.NewLine +
                "Example:" + System.Environment.NewLine +
                "   \"AssetBundleMod1,AssetBundleMod2,AssetBundleMod3\"" + System.Environment.NewLine +
                "AssetBundleMod3 overwrites the assetbundles of AssetBundleMod2 which overwrites the assetbundles of AssetBundleMod1");
            ConfigEnableSpriteAnimationMods = Config.Bind<bool>("Animation", "EnableSpriteAnimationMods", true, "Set to true to enable SpriteAnimation mods, false to completely disable them");
            ConfigSpriteAnimationModFolders = Config.Bind<string>("Animation", "SpriteAnimationModFolders", "Demo",
                "Name of the active SpriteAnimation mod folder(s)" + System.Environment.NewLine +
                "For loading multiple SpriteAnimation mods add them comma seperated in ascending priority" + System.Environment.NewLine +
                "Example:" + System.Environment.NewLine +
                "   \"SpriteAnimationMod1,SpriteAnimationMod2,SpriteAnimationMod3\"" + System.Environment.NewLine +
                "SpriteAnimationMod3 overwrites the sprite animations of SpriteAnimationMod2 which overwrites the sprite animations of SpriteAnimationMod1");

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            bool enableAllMods = ConfigEnableAllMods.Value;

            if (!enableAllMods)
            {

                Log.LogInfo($"All mods are disabled in the config {Config.ConfigFilePath}. No game assets will be replaced");
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

                try
                {
                    hPatchTitleCursor = Harmony.CreateAndPatchAll(typeof(Patch.PatchTitleCursor), "PatchTitleCursor");
                    hSceneLoadPatches = Harmony.CreateAndPatchAll(typeof(Patch.SceneLoadPatches), "SceneLoadPatches");
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch Textures.");
                }
            }
            else
            {
                Log.LogInfo($"Texture mods are disabled in the config {Config.ConfigFilePath}. Default game textures will be used");
            }

            //Audio
            if (ConfigEnableAudioMods.Value)
            {
                foreach (string modFolder in ConfigAudioModFolders.Value.Split(','))
                {
                    Log.LogInfo("Configured Audio Mod Directory: " + modFolder);
                    FileLoader.AudioModFolders.Add(modFolder);
                }

                AudioStore.Init();

                try
                {
                    hPatchAudioSource = Harmony.CreateAndPatchAll(typeof(Patch.PatchAudioSource), "PatchAudioSource");
                }
                catch (Exception e)
                {
                    Log.LogError(e.Message);
                    Log.LogError($"{PluginInfo.PLUGIN_GUID} failed to patch Audio.");
                }
            }
            else
            {
                Log.LogInfo($"Audio mods are disabled in the config {Config.ConfigFilePath}. Default game audio will be used");
            }

            //AssetBundles
            if (ConfigEnableAssetBundleMods.Value)
            {
                foreach (string modFolder in ConfigAssetBundleModFolders.Value.Split(','))
                {
                    Log.LogInfo("Configured AssetBundle Mod Directory: " + modFolder);
                    FileLoader.AssetBundleModFolders.Add(modFolder);
                }

                AssetBundleStore.Init();
            }
            else
            {
                Log.LogInfo($"AssetBundle mods are disabled in the config {Config.ConfigFilePath}. No AssetBundles will be loaded.");
            }

            //SpriteAnimations
            if (ConfigEnableSpriteAnimationMods.Value)
            {
                foreach (string modFolder in ConfigSpriteAnimationModFolders.Value.Split(','))
                {
                    Log.LogInfo("Configured SpriteAnimation Mod Directory: " + modFolder);
                    FileLoader.SpriteAnimationModFolders.Add(modFolder);
                }
                SpriteAnimationStore.Init();
            }
            else
            {
                Log.LogInfo($"SpriteAnimations mods are disabled in the config {Config.ConfigFilePath}. No SpriteAnimations will be loaded.");
            }
        }
    }
}
