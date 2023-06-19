namespace AssetReplacer
{
    public static class API
    {
        public static void Register(string pluginFolder, AssetType type)
        {
            switch (type)
            {
                case AssetType.Textures:
                    FileLoader.TextureModFolders.Insert(0, pluginFolder);
                    break;
                case AssetType.Audio:
                    FileLoader.AudioModFolders.Insert(0, pluginFolder);
                    break;
                default:
                    break;
            }
        }
        public static void UnRegister(string pluginFolder, AssetType type)
        {
            switch (type)
            {
                case AssetType.Textures:
                    FileLoader.TextureModFolders.Remove(pluginFolder);
                    break;
                case AssetType.Audio:
                    FileLoader.AudioModFolders.Remove(pluginFolder);
                    break;
                default:
                    break;
            }
        }

    }

    public enum AssetType
    {
        Textures,
        Audio
    }
}