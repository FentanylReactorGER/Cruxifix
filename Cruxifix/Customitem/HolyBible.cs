using System.Collections.Generic;
using Cruxifix.Configs;
using Cruxifix.Events;
using Cruxifix.Extensions;
using Cruxifix.SchematicManaging;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PluginAPI.Enums;
using PluginAPI.Events;
using DamageType = Exiled.API.Enums.DamageType;
using ExiledHandlers = Exiled.Events.Handlers;

namespace Cruxifix.Customitem
{
    [CustomItem(ItemType.Coin)]
    public class HolyBible : CustomItem
    {
        private static readonly Core Core = Plugin.Singleton.Core;
        private static readonly SchematicHolder SchematicHolder = Plugin.Singleton.SchematicHolder;
        private static readonly Config Config = Plugin.Singleton.Config;
        private static readonly Translation Translation = Plugin.Singleton.Translation;

        public override string Name { get; set; } = Translation.BibleCustomItemName;
        public override string Description { get; set; } = Translation.BibleCustomItemDescription;
        public override float Weight { get; set; } = Config.CustomItemWeight;
        public override uint Id { get; set; } = Config.BibleCustomItemID;
        public override SpawnProperties? SpawnProperties { get; set; } = Config.BibleCustomItemSpawnProperties;
        
        private bool IsAllowed { get; set; } = true;

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CustomEvents.OnSchematicItemEquipped += OnSchematicItemEquipped;
            ExiledHandlers.Player.FlippingCoin += UsingBible;
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            CustomEvents.OnSchematicItemEquipped -= OnSchematicItemEquipped;
            ExiledHandlers.Player.FlippingCoin -= UsingBible;
        }

        private void UsingBible(FlippingCoinEventArgs ev)
        {
            if (Check(ev.Item))
            {
                CustomEvents.InvokeUsingHolyBible(new UsingHolyBibleEventArgs(ev.Player, Plugin.Singleton.SchematicHolder.GetHeldSchematic(ev.Player), Id, ev.Player.CurrentItem.Base, ev, IsAllowed));
                if (IsAllowed)
                {
                    ev.IsAllowed = false;
                    Core.UseBible(ev, SchematicHolder.GetHeldSchematic(ev.Player));
                }
                else if (!IsAllowed)
                {
                    Log.Debug("UsingBibleEvent canceled!"); 
                }
            }
        }
        
        private void OnSchematicItemEquipped(object sender, PlayerEquippedSchematicItemEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem))
            {
                ev.Player.ShowMeowHint(Translation.BibleCustomItemHintUsage); // My first time making Custom Events, i know its useless here, just having fun :)
            }
        }
    }
}
