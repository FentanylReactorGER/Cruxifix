using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Spawn;using Exiled.CustomItems.API.Features;
using Exiled.API.Interfaces;
using UnityEngine;
using DamageHandlerEnum = Exiled.API.Enums.DamageType;

namespace Cruxifix.Configs
{
    public class Config : IConfig
    {
        [Description("Should the plugin be enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("Should the plugin display a debug message")]
        public bool Debug { get; set; } = false;
        
        [Description("HSM Hints Configs (ONLY APPLIES FOR MY PLUGIN AND WONT BREAK OTHER PLUGINS)")]
        public float GlobalHintDuration { get; set; } = 5f;
        public int GlobalHintSize { get; set; } = 30;
        public float GlobalHintY { get; set; } = 950f;
        
        [Description("Custom Item Sound (AudioPath will only DISPLAY your Current Path do NOT edit, edit Clip name and IF nessesary ClipPathFolder)")]
        public string ClipName { get; set; } = "CruxifixSound.ogg";
        public string ClipPathFolder { get; set; } = "audio";

        public string AudioPath => Path.Combine(Paths.Plugins, ClipPathFolder, ClipName);
        
        public int ClipDuration { get; set; } = 5;
        
        public float MaxClipRange { get; set; } = 15;
        
        public float ClipVolume { get; set; } = 1.0f;
        
        [Description("Custom Item Schematic Configs")]
        public string CustomItemSchematic { get; set; } = "Crucifix";

        [Description("Custom Item Damage Type Whitelist (Look up the Discord for more infos)")]
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
        
        [Description("Custom Item Advanced Editing (More on your Requests)")]
        
        public Vector3 CustomItemOffset { get; set; } = new Vector3(0, 0.5f, 0.43f);
        public Vector3 CustomItemRotation { get; set; } = new Vector3(-90, 0, 0);
        public Vector3 CustomItemScale { get; set; } = new Vector3(1f, 1f, 1);
        public Vector3 CustomItemScaleItem { get; set; } = new Vector3(1f, 1f, 7.4f);
        
        [Description("Custom Item Full Heal Duration")]
        public uint CustomItemHealDur { get; set; } = 2;
        
        [Description("Custom Item ID")]
        public uint CustomItemID { get; set; } = 6969;
        
        [Description("Custom Item Weight")]
        public float CustomItemWeight { get; set; } = 1;
        
        [Description("Custom Item SpawnProperties")]
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