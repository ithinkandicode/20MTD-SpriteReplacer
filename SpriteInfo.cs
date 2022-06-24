using System;
using System.Collections.Generic;

namespace SpriteReplacer
{
    enum ESubfolder
    {
        Bosses,
        CharacterPortraits,
        Characters,
        Enemy,
        FX,
        GUI,
        Icons,
        Pickups,
        Projectiles,
        Shapes,
        Summons,
        Weapons,
        World,
        xUnity
    }

    static class SpriteInfo
    {
        //TODO: complete this dictionary with all remaining Sprites
        private static Dictionary<string, ESubfolder> SpriteDict = new Dictionary<string, ESubfolder>
        {
            // Bosses
            {"ElderBrain", ESubfolder.Bosses},
            {"ElderBrain_Em", ESubfolder.Bosses},
            {"T_Shoggoth", ESubfolder.Bosses},
            {"T_ShubNiggurath", ESubfolder.Bosses},
            {"T_SpawnerMonster", ESubfolder.Bosses},
            {"WingedMonster", ESubfolder.Bosses},
            {"WingedMonster_Em", ESubfolder.Bosses},
            // CharacterPortraits
            {"T_Abby_Portrait", ESubfolder.CharacterPortraits},
            {"T_Diamond_Portrait", ESubfolder.CharacterPortraits},
            {"T_Hina_Portrait", ESubfolder.CharacterPortraits},
            {"T_Lilith_Portrait", ESubfolder.CharacterPortraits},
            {"T_Scarlett_Portrait", ESubfolder.CharacterPortraits},
            {"T_Shana_Portrait", ESubfolder.CharacterPortraits},
            {"T_Spark_Portrait", ESubfolder.CharacterPortraits},
            // Characters
            {"T_Abby", ESubfolder.Characters},
            {"T_CharacterShadow", ESubfolder.Characters},
            {"T_Diamond", ESubfolder.Characters},
            {"T_Hina", ESubfolder.Characters},
            {"T_Lilith", ESubfolder.Characters},
            {"T_Scarlett", ESubfolder.Characters},
            {"T_Shana", ESubfolder.Characters},
            {"T_Spark", ESubfolder.Characters},
            // Enemy
            {"Boomer", ESubfolder.Enemy},
            {"Boomer_Em", ESubfolder.Enemy},
            {"BrainMonster", ESubfolder.Enemy},
            {"BrainMonster_Em", ESubfolder.Enemy},
            {"EyeMonster", ESubfolder.Enemy},
            {"EyeMonsterProjecitle", ESubfolder.Enemy},
            {"EyeMonster_Em", ESubfolder.Enemy},
            {"T_Lamprey", ESubfolder.Enemy},
            {"T_LampreyEM", ESubfolder.Enemy},
            {"T_SpawnedBug", ESubfolder.Enemy},
            {"T_SpawnedBug_Em", ESubfolder.Enemy},
            {"T_TreeMonster", ESubfolder.Enemy},
            // FX
            {"ExplosionFX", ESubfolder.FX},
            {"T_DeathFX", ESubfolder.FX},
            {"T_ElectricWall1", ESubfolder.FX},
            {"T_ElectricWall2", ESubfolder.FX},
            {"T_ElectricWall3", ESubfolder.FX},
            {"T_ElectricWall4", ESubfolder.FX},
            {"T_ElectricWall5", ESubfolder.FX},
            {"T_ElectricWall6", ESubfolder.FX},
            {"T_FireballExplosion", ESubfolder.FX},
            {"T_FireExplosionSmall", ESubfolder.FX},
            {"T_FreezeFXLarge", ESubfolder.FX},
            {"T_FreezeFXSmall", ESubfolder.FX},
            {"T_Guardian_SummonDeathFX", ESubfolder.FX},
            {"T_HolyShield", ESubfolder.FX},
            {"T_HolyShieldBreak", ESubfolder.FX},
            {"T_HolyShieldRegen", ESubfolder.FX},
            {"T_LevelUpFX", ESubfolder.FX},
            {"T_ShadowCloneAttackFX", ESubfolder.FX},
            {"T_ShatterParticles", ESubfolder.FX},
            {"T_ShockwaveFX", ESubfolder.FX},
            {"T_ShoggothLaser", ESubfolder.FX},
            {"T_ShoggothLaserWindup", ESubfolder.FX},
            {"T_StaticParticles", ESubfolder.FX},
            {"T_Thunder", ESubfolder.FX},
            // GUI
            {"T_20Logo", ESubfolder.GUI},
            {"T_AmmoIcon", ESubfolder.GUI},
            {"T_Cursor", ESubfolder.GUI},
            {"T_CursorSprite", ESubfolder.GUI},
            {"T_EyeBlink", ESubfolder.GUI},
            {"T_HeartAnimation", ESubfolder.GUI},
            {"T_LargeChestAnimation", ESubfolder.GUI},
            {"T_PlayButton", ESubfolder.GUI},
            {"T_PowerupPanel", ESubfolder.GUI},
            {"T_PowerupTreeArrows", ESubfolder.GUI},
            {"T_ReloadBar", ESubfolder.GUI},
            {"T_SelectorBubble", ESubfolder.GUI},
            {"T_SelectScreenPanel", ESubfolder.GUI},
            {"T_SoulIcon", ESubfolder.GUI},
            {"T_TitleLeaves", ESubfolder.GUI},
            {"T_UILock", ESubfolder.GUI},
            {"T_UIPanel", ESubfolder.GUI},
            {"T_VictoryAchievedIcon", ESubfolder.GUI},
            {"T_WorldArrow", ESubfolder.GUI},
            {"UIMask", ESubfolder.GUI},
            // Icons
            {"T_Powerups", ESubfolder.Icons},
            {"T_RuneIcons", ESubfolder.Icons},
            // Pickups
            {"T_Chest", ESubfolder.Pickups},
            {"T_DevilDealPickup", ESubfolder.Pickups},
            {"T_HeartPickup", ESubfolder.Pickups},
            {"T_ShanaHalo", ESubfolder.Pickups},
            // Projectiles
            {"T_BatgunProjectile", ESubfolder.Projectiles},
            {"T_FireParticles", ESubfolder.Projectiles},
            {"T_FireWave", ESubfolder.Projectiles},
            {"T_GunFX", ESubfolder.Projectiles},
            {"T_Icicle", ESubfolder.Projectiles},
            // Shapes
            {"Soft", ESubfolder.Shapes},
            {"T_Circle", ESubfolder.Shapes},
            {"T_RoundedRect", ESubfolder.Shapes},
            {"T_SmallCircle", ESubfolder.Shapes},
            {"T_UISmallPanel", ESubfolder.Shapes},
            // Summons
            {"T_DragonEgg", ESubfolder.Summons},
            {"T_DragonSS", ESubfolder.Summons},
            {"T_ElectroBug_SS", ESubfolder.Summons},
            {"T_GhostPet_SS", ESubfolder.Summons},
            {"T_GunGlyph", ESubfolder.Summons},
            {"T_Knife", ESubfolder.Summons},
            {"T_MagicLens", ESubfolder.Summons},
            {"T_Scythe", ESubfolder.Summons},
            {"T_SeismicWard", ESubfolder.Summons},
            {"T_ShadowClone", ESubfolder.Summons},
            {"T_SpiritSkull", ESubfolder.Summons},
            // Weapons
            {"T_Batgun_SS", ESubfolder.Weapons},
            {"T_Crossbow_SS", ESubfolder.Weapons},
            {"T_DualSMGs_SS", ESubfolder.Weapons},
            {"T_FlameCannon_SS", ESubfolder.Weapons},
            {"T_GrenadeLauncher_SS", ESubfolder.Weapons},
            {"T_Revolver_SS", ESubfolder.Weapons},
            {"T_Shotgun_SS", ESubfolder.Weapons},
            // World
            {"T_TileGrass", ESubfolder.World},
            // xUnity
            {"box", ESubfolder.xUnity},
            {"button active", ESubfolder.xUnity},
            {"button hover", ESubfolder.xUnity},
            {"button on hover", ESubfolder.xUnity},
            {"button on", ESubfolder.xUnity},
            {"button", ESubfolder.xUnity},
            {"horizontal scrollbar thumb", ESubfolder.xUnity},
            {"horizontal scrollbar", ESubfolder.xUnity},
            {"horizontalslider", ESubfolder.xUnity},
            {"slider thumb active", ESubfolder.xUnity},
            {"slider thumb", ESubfolder.xUnity},
            {"slidert humb hover", ESubfolder.xUnity},
            {"textfield hover", ESubfolder.xUnity},
            {"textfield on", ESubfolder.xUnity},
            {"textfield", ESubfolder.xUnity},
            {"toggle active", ESubfolder.xUnity},
            {"toggle hover", ESubfolder.xUnity},
            {"toggle on active", ESubfolder.xUnity},
            {"toggle on hover", ESubfolder.xUnity},
            {"toggle on", ESubfolder.xUnity},
            {"toggle", ESubfolder.xUnity},
            {"UnitySplash-cube", ESubfolder.xUnity},
            {"UnitySplash-HolographicTrackingLoss", ESubfolder.xUnity},
            {"UnityWatermark-beta", ESubfolder.xUnity},
            {"UnityWatermark-dev", ESubfolder.xUnity},
            {"UnityWatermark-edu", ESubfolder.xUnity},
            {"UnityWatermark-proto", ESubfolder.xUnity},
            {"UnityWatermark-small", ESubfolder.xUnity},
            {"UnityWatermark-trial-big", ESubfolder.xUnity},
            {"UnityWatermark-trial", ESubfolder.xUnity},
            {"UnityWatermarkPlugin-beta", ESubfolder.xUnity},
            {"vertical scrollbar thumb", ESubfolder.xUnity},
            {"vertical scrollbar", ESubfolder.xUnity},
            {"verticalslider", ESubfolder.xUnity},
            {"WarningSign", ESubfolder.xUnity},
            {"window on", ESubfolder.xUnity},
            {"window", ESubfolder.xUnity},
        };

        public static string GetSubFolder(string spriteName)
        {
            if (SpriteDict.ContainsKey(spriteName))
            {
                return Enum.GetName(typeof(ESubfolder), SpriteDict[spriteName]);
            }
            else
            {
                return "";
            }
        }

    }
}