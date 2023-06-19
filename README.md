# 20MTD AssetReplacer
BepInEx plugin to replace assets in 20 Minutes Till Dawn.

Can currently replace:

- Textures of Sprites
- Audio

## Usage

### Thunderstore
1. Install through Thunderstore
2. Install any Asset-Mods based on AssetReplacer
2. See Configuration

### Manual
1. Install BepInEx
2. Add the plugin DLL to BepInEx\plugins
3. Install any Asset-Mods based on AssetReplacer
<br>They should follow this structure:
```
20MinutesTillDawn
  |--BepInEx
      |--plugins
          |--YourModFolderName
              |--Textures
              |   |--Enemies
              |   |   |EyeMonster.png
              |   |   |T_TreeMonster.png
              |   |--Projectiles
              |       |T_GunFX.png
              |       |T_FireParticles.png
              |--Audio
                  |title.mp3
                  |battle.mp3
```
"Textures" and "Audio" may only contain files corresponding to the type. E.g. Textures should not contain any text files, only images.
<br>Files in "Textures" and "Audio" may follow any structure. They may be located in subdirectories or directly contained in the folders.

4. See Configuration

## Configuration
### Thunderstore
1. Enable/Disable which assets to replace
2. Add the Mods directory names to the proper ModFolders configuration comma seperated
	<br>Thunderstore Format: ModCreator-Modname
	<br>Example:
	```
	[Textures]

	EnableTextureMods = true

	TextureModFolders = Zeprus-ExampleTextureMod,Zeprus-ExampleTextureMod2
	```
### Manual
2. Add the Mods directory names to the proper ModFolders configuration comma seperated
	<br>Format: ModfolderName
	<br>Example:
	<br>Mod folder is BepInEx/plugins/YourModFolderName
	```
	[Textures]

	EnableTextureMods = true

	TextureModFolders = YourModFolderName
	```

## Setup

1. Clone the repo
2. Add libs to /lib:
	- Add all the DLLs from the game's Managed folder
	- Add all BepInEx DLLs
3. Install any pre-requisite stuff (see [BepInEx setup docs](https://docs.bepinex.dev/articles/dev_guide/plugin_tutorial/1_setup.html))
3. Open in [Visual Studio](https://visualstudio.microsoft.com/vs/community/)
4. Build via Build » Build Solution
	- Compiled DLL will be in /bin/Debug

## Create Asset-Mods

### Thunderstore
1. Create your [Thunderstore package](https://github.com/ebkr/r2modmanPlus/wiki/Structuring-your-Thunderstore-package)
2. Create a "plugins" folder inside your package
3. Create a folder inside plugins named after the asset type you want to replace (e.g. textures or audio)
4. Place your replacement assets inside the proper subfolder following any structure
5. (Optional) Write a BepInEx-Plugin that calls AssetReplacer.API.Register("Author-Modfoldername", AssetReplacer.API.AssetType.\[Textures/Audio\]) during its Awake() function and has AssetReplacer as dependency. See example plugin.
\nStructure examples:
```
MyThunderStorePackage.zip
|README.md
|icon.png
|manifest.json
|--Plugins
	|--Textures
	|	|--Enemies
	|	|	|EyeMonster.png
	|	|	|T_TreeMonster.png
	|	|
	|	|--Projectiles
	|		|T_GunFX.png
	|		|T_FireParticles.png
	|--Audio
		|title.mp3
		|battle.mp3
```
```
MyThunderStorePackage.zip
|README.md
|icon.png
|manifest.json
|--Plugins
	|--Textures
	|	|EyeMonster.png
	|	|T_TreeMonster.png
	|	|T_GunFX.png
	|	|T_FireParticles.png
	|
	|--Audio
		|title.mp3
```
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

## Changelog
- 20.2.0 added API for registering Modfolders programmatically 
- 20.1.0 Bugfixes for 20MTD v1.0 release, increased stability
- 20.0.0 Thunderstore release