namespace AssetReplacer
{
    public class API
    {
        public void Register(string pluginFolder, string type)
        {
            switch (type)
            {
                case "Textures":
                    FileLoader.TextureModFolders.Insert(0, pluginFolder);
                    break;
                case "Audio":
                    FileLoader.AudioModFolders.Insert(0, pluginFolder);
                    break;
                default:
                    break;
            }
        }
        public void UnRegister(string pluginFolder, string type)
        {
            switch (type)
            {
                case "Textures":
                    FileLoader.TextureModFolders.Remove(pluginFolder);
                    break;
                case "Audio":
                    FileLoader.AudioModFolders.Remove(pluginFolder);
                    break;
                default:
                    break;
            }
        }

    }
}