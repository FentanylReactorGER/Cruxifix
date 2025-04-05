using System.Collections.Generic;
using Cruxifix.Configs;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using GameCore;
using PluginAPI.Enums;
using PluginAPI.Events;
using DamageType = Exiled.API.Enums.DamageType;
using ExiledHandlers = Exiled.Events.Handlers;
using Log = Exiled.API.Features.Log;

namespace Cruxifix.Customitem
{
    [CustomItem(ItemType.Coin)]
    public class Curxifix : CustomItem
    {
        private static readonly Core Core = Plugin.Singleton.Core;
        private static readonly Config Config = Plugin.Singleton.Config;
        public List<DamageType> CustomItemDamageTypes = Config.CustomItemDamageTypes;
        private static readonly Translation Translation = Plugin.Singleton.Translation;
        public override string Name { get; set; } = Translation.CustomItemName;
        public override string Description { get; set; } = Translation.CustomItemDescription;
        public override float Weight { get; set; } = Config.CustomItemWeight;
        public override uint Id { get; set; } = Config.CustomItemID;
        public override SpawnProperties? SpawnProperties { get; set; } = Config.CustomItemSpawnProperties;

        protected override void SubscribeEvents()
        {
            ExiledHandlers.Player.Dying += UsingCruxifix;
            ExiledHandlers.Player.UsingItem += BlockThrowEvents;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            ExiledHandlers.Player.Dying -= UsingCruxifix;
            ExiledHandlers.Player.UsingItem -= BlockThrowEvents;
            base.UnsubscribeEvents();
        }

        private void UsingCruxifix(DyingEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem)) 
            {
                Log.Debug($"Player {ev.Player.Nickname} dying Event, Type: {ev.DamageHandler.Type} and Suicide: {ev.DamageHandler.IsSuicide}. Is in Config: {CustomItemDamageTypes.Contains(ev.DamageHandler.Type)}");
               if (CustomItemDamageTypes.Contains(ev.DamageHandler.Type))
                {
                    Core.CruxifixAbility(ev);
                }
            }
        }

        private void BlockThrowEvents(UsingItemEventArgs ev)
        {
            if (Check(ev.Item))
            {
                ev.IsAllowed = false;
                ev.IsAllowed = true;
            }
        }
    }
}