using System.Collections.Generic;
using Cruxifix.Configs;
using Exiled.API.Enums;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Spawn;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using PluginAPI.Enums;
using PluginAPI.Events;
using DamageType = Exiled.API.Enums.DamageType;
using ExiledHandlers = Exiled.Events.Handlers;

namespace Cruxifix
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
            if (Check(ev.Player.CurrentItem) && !ev.DamageHandler.IsSuicide) // Suicide is weird and idk if it works (DONT DO SUICIDE GUYS IT WONT WORK!!)
            {
                if (ev.DamageHandler.Type == DamageType.Falldown && ev.Player.CurrentRoom.Type == RoomType.Surface) // So player doesn't jumps off in the Facility and gets trapped, but gets saved at surface :)
                {
                    Core.CruxifixAbility(ev);
                }
                else if (CustomItemDamageTypes.Contains(ev.DamageHandler.Type))
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