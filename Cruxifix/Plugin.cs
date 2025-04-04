using System;
using System.IO;
using Cruxifix.Configs;
using Cruxifix.SchematicManaging;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using UnityEngine;
using Random = System.Random;

namespace Cruxifix
{
    public class Plugin : Plugin<Configs.Config, Configs.Translation>
    {
        public override string Name => "Cruxifix";
        public override string Author => "Whoever reads this; Jesus loves you!";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 6, 4);
        
        public static Plugin Singleton { get; private set; }
        public static readonly Random Random = new Random();

        public SchematicHandler SchematicHandler { get; private set; }
        public SchematicHolder SchematicHolder { get; private set; }
        public Core Core { get; private set; }

        private Vector3 scale;
        private Vector3 Offset;
        private Quaternion Rotation;

        public override void OnEnabled()
        {
            Singleton = this;

            Core = new Core();
            scale = this.Config.CustomItemScale;
            Offset = this.Config.CustomItemOffset;
            Rotation = Quaternion.Euler(this.Config.CustomItemRotation);

            SchematicHolder = new SchematicHolder(scale, Offset, Rotation);
            Exiled.Events.Handlers.Player.ChangedItem += SchematicHolder.OnChangedItem;

            SchematicHandler = new SchematicHandler();
            SchematicHandler.SubscribeEvents();

            string audioDirectory = Path.Combine(Paths.Plugins, Config.ClipPathFolder);
            if (!Directory.Exists(audioDirectory))
                Directory.CreateDirectory(audioDirectory);

            CustomItem.RegisterItems();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.ChangedItem -= SchematicHolder.OnChangedItem;

            Core = null;
            SchematicHolder = null;

            SchematicHandler.UnsubscribeEvents();
            SchematicHandler = null;

            Singleton = null;

            CustomItem.UnregisterItems();
            base.OnDisabled();
        }
    }
}
