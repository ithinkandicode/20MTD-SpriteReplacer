using BepInEx;
using System.IO;
using System.Reflection;

namespace Example
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("AssetReplacer")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            string modFolder = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Name;
            AssetReplacer.API.Register(modFolder, AssetReplacer.AssetType.Textures);
            AssetReplacer.API.Register(modFolder, AssetReplacer.AssetType.Audio);
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
