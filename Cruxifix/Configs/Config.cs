using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Cruxifix.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.API.Interfaces;
using Scp914;
using UnityEngine;
using DamageHandlerEnum = Exiled.API.Enums.DamageType;

namespace Cruxifix.Configs
{
    public class Config : IConfig
    {
        // === Plugin Settings ===
        [Description("Should the plugin be enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should the plugin display a debug message")]
        public bool Debug { get; set; } = false;

        // === Hint Settings ===
        [Description("HSM Hints Configs (ONLY APPLIES FOR MY PLUGIN AND WONT BREAK OTHER PLUGINS)")]
        public float GlobalHintDuration { get; set; } = 5f;
        public int GlobalHintSize { get; set; } = 30;
        public float GlobalHintY { get; set; } = 950f;

        // === Audio Settings ===
        [Description("Custom Item Sound (AudioPath will only DISPLAY your Current Path do NOT edit, edit Clip name and IF necessary ClipPathFolder)")]
        public string ClipName { get; set; } = "CruxifixSound.ogg";
        public string ClipNameBible { get; set; } = "BibleSound.ogg";
        public string ClipPathFolder { get; set; } = "audio";
        public int ClipDuration { get; set; } = 5;
        public float MaxClipRange { get; set; } = 15;
        public float ClipVolume { get; set; } = 1.0f;

        // === 914 Crafting Settings ===
        [Description("Custom Item 914 Configs (Won't destroy basegame but might conflict with other 914 Plugins)")]
        public bool Custom914 { get; set; } = true;

        public List<CustomClasses.CustomItemRecipes> CustomItemRecipeDictionary { get; set; } = new()
        {
            new CustomClasses.CustomItemRecipes(ItemType.KeycardO5, 6969, Scp914KnobSetting.Fine, 75),
            new CustomClasses.CustomItemRecipes(6969, ItemType.Coin, Scp914KnobSetting.Coarse, 20),
            new CustomClasses.CustomItemRecipes(6969, ItemType.Adrenaline, Scp914KnobSetting.OneToOne, 40),
            new CustomClasses.CustomItemRecipes(6969, ItemType.SCP500, Scp914KnobSetting.Fine, 30),
            new CustomClasses.CustomItemRecipes(6969, 6700, Scp914KnobSetting.VeryFine, 15),
            new CustomClasses.CustomItemRecipes(ItemType.Coin, 6969, Scp914KnobSetting.VeryFine, 5),
            new CustomClasses.CustomItemRecipes(6969, 6999, Scp914KnobSetting.Coarse, 30),
            new CustomClasses.CustomItemRecipes(6969, 6969, Scp914KnobSetting.OneToOne, 10),
            new CustomClasses.CustomItemRecipes(1488, 6969, Scp914KnobSetting.Fine, 20),
        };

        // === Schematic Settings ===
        [Description("Custom Item Schematic Configs")]
        public string CustomItemSchematic { get; set; } = "Crucifix";
        
        [Description("Custom Item Placer Transparent (Max: 1f)")]
        public float CustomItemTransparent { get; set; } = 0.3f;
        
        [Description("Custom Item Placer Max Range (1f = 1 Meter)")]
        public float CustomItemMaxRange { get; set; } = 25f;
        
        [Description("Burning Crucifx Color")]
        public Color BurningCrucifixColor { get; set; } = new Color(1f, 0f, 0f, 1f);

        [Description("Custom Schematic Item List (Can add Custom Items here from other Plugins)")]
        public List<CustomClasses.SchematicItems> CustomSchematicItems { get; set; } = new()
        {
            new CustomClasses.SchematicItems("Crucifix", 6969, new Vector3(1f, 1f, 7.4f), ""),
            new CustomClasses.SchematicItems("HolyBible", 6700, new Vector3(6.7f, 1, 4.2f), "")
        };

        // === Custom Item Effects ===
        [Description("Custom Item Effects (Effect Duration is decided by 'CustomItemHealDur')")]
        public List<EffectType> CustomItemEffects { get; set; } = new()
        {
            EffectType.Asphyxiated,
            EffectType.Flashed,
            EffectType.Blurred,
            EffectType.Concussed
        };

        [Description("Custom Item Damage Type Whitelist")]
        public List<DamageType> CustomItemDamageTypes { get; set; } = new()
        {
            DamageType.Com15,
            DamageType.Com18,
            DamageType.Crossvec,
            DamageType.Logicer,
            DamageType.ParticleDisruptor,
            DamageType.Shotgun,
            DamageType.Jailbird,
            DamageType.Revolver,
            DamageType.Scp018,
            DamageType.Scp049,
            DamageType.Scp0492,
            DamageType.Scp096,
            DamageType.Scp106,
            DamageType.Scp173,
            DamageType.Scp939,
            DamageType.Scp207,
            DamageType.PocketDimension,
            DamageType.Tesla,
            DamageType.Decontamination,
            DamageType.Asphyxiation,
            DamageType.Recontainment,
            DamageType.Poison,
            DamageType.Bleeding,
            DamageType.Explosion,
            DamageType.Frmg0,
            DamageType.Fsp9,
            DamageType.CardiacArrest,
        };

        // === Bible Settings ===
        [Description("Effects the Bible cures when applied")]
        public List<EffectType> BibleHealEffectList { get; set; } = new()
        {
            EffectType.Bleeding,
            EffectType.Corroding,
            EffectType.Poisoned,
            EffectType.Scp207,
            EffectType.Scp1853,
            EffectType.SeveredHands,
            EffectType.CardiacArrest,
            EffectType.SeveredEyes
        };

        public uint BibleCustomItemID { get; set; } = 6700;
        public float BibleCustomItemRange { get; set; } = 15f;

        public List<EffectType> BibleCustomItemEffects { get; set; } = new()
        {
            EffectType.Asphyxiated,
            EffectType.Flashed,
            EffectType.Blurred,
            EffectType.Concussed
        };

        public float BibleCustomEffectDur { get; set; } = 5f;
        public string BibleCustomSchematicName { get; set; } = "HolyBible";
        public string BibleCustomAnimationName { get; set; } = "BibleAnimation";
        public string BibleCustomAnimatorName { get; set; } = "Animation";

        public SpawnProperties BibleCustomItemSpawnProperties { get; set; } = new()
        {
            Limit = 7,
            LockerSpawnPoints = new()
            {
                new LockerSpawnPoint()
                {
                    Chance = 70,
                    Type = LockerType.Medkit,
                    UseChamber = true,
                    Offset = Vector3.zero,
                },
                new LockerSpawnPoint()
                {
                    Chance = 100,
                    Type = LockerType.Misc,
                    UseChamber = true,
                    Offset = Vector3.zero,
                }
            },
        };

        // === Custom Item Advanced Settings ===
        [Description("Custom Item Advanced Editing (More on your Requests)")]
        public Vector3 CustomItemOffset { get; set; } = new Vector3(0, 0.5f, 0.43f);
        public Vector3 CustomItemRotation { get; set; } = new Vector3(-90, 0, 0);
        public Vector3 CustomItemScale { get; set; } = new Vector3(1f, 1f, 1);

        [Description("Custom Item Full Heal Duration")]
        public uint CustomItemHealDur { get; set; } = 2;

        [Description("Custom Item ID")]
        public uint CustomItemID { get; set; } = 6969;

        [Description("Custom Items Weight")]
        public float CustomItemWeight { get; set; } = 1;

        [Description("Custom Items SpawnProperties")]
        public SpawnProperties CustomItemSpawnProperties { get; set; } = new()
        {
            Limit = 7,
            LockerSpawnPoints = new()
            {
                new LockerSpawnPoint()
                {
                    Chance = 70,
                    Type = LockerType.Medkit,
                    UseChamber = true,
                    Offset = Vector3.zero,
                },
                new LockerSpawnPoint()
                {
                    Chance = 100,
                    Type = LockerType.Misc,
                    UseChamber = true,
                    Offset = Vector3.zero,
                }
            },
        };
    }
}
