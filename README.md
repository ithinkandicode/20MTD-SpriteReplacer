# 20MTD AssetReplacer
BepInEx plugin to replace assets in 20 Minutes Till Dawn.

Can currently replace:

- Textures of Sprites
- Audio

## Usage

1. Install BepInEx
1. Add the plugin DLL to BepInEx\plugins
2. Create a folder in BepInEx\plugins for your mod, without spaces (eg `MyTextureMod`).
3. In your mods folder create folders for your files called `Textures` and/or `Audio`
4. Add your texture or audio files to the respective folder using [this folder structure](https://github.com/ithinkandicode/20MTD-Graphical-Overhaul/blob/main/filetree.txt)
5. Update the plugin config to point to your folder
  - Config coming soon...


## Setup

1. Clone the repo
2. Add libs to /lib:
	- Add all the DLLs from the game's Managed folder
	- Add all BepInEx DLLs
3. Install any pre-requisite stuff (see [BepInEx setup docs](https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/1_setup.html))
3. Open in [Visual Studio](https://visualstudio.microsoft.com/vs/community/)
4. Build via Build » Build Solution
	- Compiled DLL will be in /bin/Debug

## Links

- [Steam](https://store.steampowered.com/app/1966900/20_Minutes_Till_Dawn/)
- [Nexus Mods](https://www.nexusmods.com/20minutestildawn)
- [Discord](https://discord.gg/DtSPxBXtWJ)
- [Discord » Modding Channel](https://discord.com/channels/976039553683034122/987507054082162758)
- [Wiki » Modding](https://minutes-till-dawn.fandom.com/wiki/Modding)
- [BepInEx Github](https://github.com/BepInEx/BepInEx/releases)
- [BepInEx Docs](https://docs.bepinex.dev/index.html)


## Credits

- [TormentedEmu/7DTD-A19-DMTMods](https://github.com/TormentedEmu/7DTD-A19-DMTMods/blob/master/TE_MenuMusic/Harmony/Harmony.cs) - Code for the music loader
