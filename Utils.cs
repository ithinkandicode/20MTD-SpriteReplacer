using System;
using System.IO;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using flanne.UI;
using static SpriteReplacer.SpriteReplacer;

namespace SpriteReplacer
{
    public class Utils
    {
        // Returns true on successful patch, false otherwise
        static public bool ReplaceSpriteTexture(Sprite targetSprite)
        {
            if (targetSprite != null)
            {
                //Log.LogDebug("Sprite.name:" + targetSprite.name);

                Texture2D spriteTexture = targetSprite.texture;

                if (spriteTexture != null)
                {
                    string subfolder = Utils.GetTextureSubfolder(spriteTexture.name);
                    string modPath = SpriteReplacer.configTextureModFolder.Value;
                    string path = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Mods", "Textures", modPath, subfolder, spriteTexture.name + ".png");

                    //Log.LogDebug("Sprite.Texture.name:" + spriteTexture.name);
                    //Log.LogDebug("SearchPath:" + path);

                    if (File.Exists(path))
                    {
                        Sprite ogSprite = targetSprite;
                        ogSprite.texture.LoadImage(File.ReadAllBytes(path));
                        Vector2 standardisedPivot = new Vector2(ogSprite.pivot.x / ogSprite.rect.width, ogSprite.pivot.y / ogSprite.rect.height);
                        Sprite sprite = Sprite.Create(ogSprite.texture, ogSprite.rect, standardisedPivot, ogSprite.pixelsPerUnit);
                        sprite.name = ogSprite.name;
                        spriteTexture = sprite.texture;

                        Log.LogDebug("OK! Replaced: " + path);

                        return true;
                    }
                    else
                    {
                        Log.LogDebug("FAIL! No image at: " + path);
                    }
                }
            }

            return false;
        }

        // Returns the subfolder to the sprite (if available), otherwise returns an empty string
        // Note: Yes this is innefficient, but the game only has ~100 sprites ATOW
        static public string GetTextureSubfolder(string spriteName)
        {
            string subfolder = "";

            switch (spriteName)
            {
                // Bosses
                case "ElderBrain":
                case "ElderBrain_Em":
                case "T_Shoggoth":
                case "T_ShubNiggurath":
                case "T_SpawnerMonster":
                case "WingedMonster":
                case "WingedMonster_Em":
                    subfolder = "Bosses";
                    break;

                // CharacterPortraits
                case "T_Abby_Portrait":
                case "T_Diamond_Portrait":
                case "T_Hina_Portrait":
                case "T_Lilith_Portrait":
                case "T_Scarlett_Portrait":
                case "T_Shana_Portrait":
                case "T_Spark_Portrait":
                    subfolder = "CharacterPortraits";
                    break;

                // Characters
                case "T_Abby":
                case "T_CharacterShadow":
                case "T_Diamond":
                case "T_Hina":
                case "T_Lilith":
                case "T_Scarlett":
                case "T_Shana":
                case "T_Spark":
                    subfolder = "Characters";
                    break;

                // Enemy
                case "Boomer":
                case "Boomer_Em":
                case "BrainMonster":
                case "BrainMonster_Em":
                case "EyeMonster":
                case "EyeMonsterProjecitle":
                case "EyeMonster_Em":
                case "T_Lamprey":
                case "T_LampreyEM":
                case "T_SpawnedBug":
                case "T_SpawnedBug_Em":
                case "T_TreeMonster":
                    subfolder = "Enemy";
                    break;

                // FX
                case "ExplosionFX":
                case "T_DeathFX":
                case "T_ElectricWall1":
                case "T_ElectricWall2":
                case "T_ElectricWall3":
                case "T_ElectricWall4":
                case "T_ElectricWall5":
                case "T_ElectricWall6":
                case "T_FireballExplosion":
                case "T_FireExplosionSmall":
                case "T_FreezeFXLarge":
                case "T_FreezeFXSmall":
                case "T_Guardian_SummonDeathFX":
                case "T_HolyShield":
                case "T_HolyShieldBreak":
                case "T_HolyShieldRegen":
                case "T_LevelUpFX":
                case "T_ShadowCloneAttackFX":
                case "T_ShatterParticles":
                case "T_ShockwaveFX":
                case "T_ShoggothLaser":
                case "T_ShoggothLaserWindup":
                case "T_StaticParticles":
                case "T_Thunder":
                    subfolder = "FX";
                    break;

                // GUI
                case "T_20Logo":
                case "T_AmmoIcon":
                case "T_Cursor":
                case "T_CursorSprite":
                case "T_EyeBlink":
                case "T_HeartAnimation":
                case "T_LargeChestAnimation":
                case "T_PlayButton":
                case "T_PowerupPanel":
                case "T_PowerupTreeArrows":
                case "T_ReloadBar":
                case "T_SelectorBubble":
                case "T_SelectScreenPanel":
                case "T_SoulIcon":
                case "T_TitleLeaves":
                case "T_UILock":
                case "T_UIPanel":
                case "T_VictoryAchievedIcon":
                case "T_WorldArrow":
                case "UIMask":
                    subfolder = "GUI";
                    break;

                // Icons
                case "T_Powerups":
                case "T_RuneIcons":
                    subfolder = "Icons";
                    break;

                // Pickups
                case "T_Chest":
                case "T_DevilDealPickup":
                case "T_HeartPickup":
                case "T_ShanaHalo":
                    subfolder = "Pickups";
                    break;

                // Projectiles
                case "T_BatgunProjectile":
                case "T_FireParticles":
                case "T_FireWave":
                case "T_GunFX":
                case "T_Icicle":
                    subfolder = "Projectiles";
                    break;

                // Shapes
                case "Soft":
                case "T_Circle":
                case "T_RoundedRect":
                case "T_SmallCircle":
                case "T_UISmallPanel":
                    subfolder = "Shapes";
                    break;

                // Summons
                case "T_DragonEgg":
                case "T_DragonSS":
                case "T_ElectroBug_SS":
                case "T_GhostPet_SS":
                case "T_GunGlyph":
                case "T_Knife":
                case "T_MagicLens":
                case "T_Scythe":
                case "T_SeismicWard":
                case "T_ShadowClone":
                case "T_SpiritSkull":
                    subfolder = "Summons";
                    break;

                // Weapons
                case "T_Batgun_SS":
                case "T_Crossbow_SS":
                case "T_DualSMGs_SS":
                case "T_FlameCannon_SS":
                case "T_GrenadeLauncher_SS":
                case "T_Revolver_SS":
                case "T_Shotgun_SS":
                    subfolder = "Weapons";
                    break;

                // World
                case "T_TileGrass":
                    subfolder = "World";
                    break;

                // xUnity
                case "box":
                case "button active":
                case "button hover":
                case "button on hover":
                case "button on":
                case "button":
                case "horizontal scrollbar thumb":
                case "horizontal scrollbar":
                case "horizontalslider":
                case "slider thumb active":
                case "slider thumb":
                case "slidert humb hover":
                case "textfield hover":
                case "textfield on":
                case "textfield":
                case "toggle active":
                case "toggle hover":
                case "toggle on active":
                case "toggle on hover":
                case "toggle on":
                case "toggle":
                case "UnitySplash-cube":
                case "UnitySplash-HolographicTrackingLoss":
                case "UnityWatermark-beta":
                case "UnityWatermark-dev":
                case "UnityWatermark-edu":
                case "UnityWatermark-proto":
                case "UnityWatermark-small":
                case "UnityWatermark-trial-big":
                case "UnityWatermark-trial":
                case "UnityWatermarkPlugin-beta":
                case "vertical scrollbar thumb":
                case "vertical scrollbar":
                case "verticalslider":
                case "WarningSign":
                case "window on":
                case "window":
                    subfolder = "xUnity";
                    break;
            }

            return subfolder;
        }
    }
}
