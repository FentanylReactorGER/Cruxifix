using System.Collections.Generic;
using System.Linq;
using Cruxifix.Configs;
using Cruxifix.SchematicManaging;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
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
        private static readonly SchematicPlacer SchematicPlacer = Plugin.Singleton.SchematicPlacer;
        private static readonly SchematicHolder SchematicHolder = Plugin.Singleton.SchematicHolder;
        private static readonly Core Core = Plugin.Singleton.Core;
        private static readonly Config Config = Plugin.Singleton.Config;
        public List<DamageType> CustomItemDamageTypes = Config.CustomItemDamageTypes;
        private static readonly Translation Translation = Plugin.Singleton.Translation;
        public override string Name { get; set; } = Translation.CustomItemName;
        public override string Description { get; set; } = Translation.CustomItemDescription;
        public override float Weight { get; set; } = Config.CustomItemWeight;
        
        private Dictionary<Player, bool> CrucifixPlacers = new Dictionary<Player, bool>();
        public override uint Id { get; set; } = Config.CustomItemID;
        public override SpawnProperties? SpawnProperties { get; set; } = Config.CustomItemSpawnProperties;
        private bool IsAllowed { get; set; } = true;

        protected override void SubscribeEvents()
        {
            ExiledHandlers.Player.ChangingItem += DestroyingPlacedCrucfix;
            ExiledHandlers.Player.Dying += UsingCruxifix;
            ExiledHandlers.Player.FlippingCoin += BlockThrowEvents;
            ExiledHandlers.Player.TogglingNoClip += PlaceCrucifx;
            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            ExiledHandlers.Player.ChangingItem -= DestroyingPlacedCrucfix;
            ExiledHandlers.Player.Dying -= UsingCruxifix;
            ExiledHandlers.Player.FlippingCoin -= BlockThrowEvents;
            ExiledHandlers.Player.TogglingNoClip -= PlaceCrucifx;
            base.UnsubscribeEvents();
        }

        private void DestroyingPlacedCrucfix(ChangingItemEventArgs ev)
        {
            if (CrucifixPlacers.TryGetValue(ev.Player, out bool crucifix) && crucifix)
            {
                CrucifixPlacers[ev.Player] = false;
                SchematicPlacer.StopPreview(ev.Player);
            }
        }
        
        private void PlaceCrucifx(TogglingNoClipEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem))
            {
                if (CrucifixPlacers.TryGetValue(ev.Player, out bool crucifix) && crucifix)
                {
                    CrucifixPlacers[ev.Player] = false;
                    SchematicPlacer.PlaceSchematic(ev.Player, Config.CustomItemSchematic);
                    SchematicPlacer.StopPreview(ev.Player);
                    foreach (Item item in ev.Player.Items)
                    {
                        if (Check(item))
                        {
                            item.Destroy();
                            break;
                        }
                    }
                }
                else if (CrucifixPlacers.TryGetValue(ev.Player, out bool crucifixNotUsed) && !crucifixNotUsed)
                {
                    CrucifixPlacers[ev.Player] = true;
                    SchematicHolder.DestroyHeld(ev.Player);
                    SchematicPlacer.StartPreview(ev.Player, Config.CustomItemSchematic);
                }
                else
                {
                    CrucifixPlacers[ev.Player] = true;
                    SchematicHolder.DestroyHeld(ev.Player);
                    SchematicPlacer.StartPreview(ev.Player, Config.CustomItemSchematic);
                }
            }
        }
        
        private void UsingCruxifix(DyingEventArgs ev)
        {
            if (Check(ev.Player.CurrentItem)) 
            {
                Events.CustomEvents.InvokeUsingCrucifix(
                    new Events.UsingCrucfixEventArgs(ev.Player, Plugin.Singleton.SchematicHolder.GetHeldSchematic(ev.Player), Id, ev.Player.CurrentItem.Base, ev, IsAllowed)
                );
                if (IsAllowed)
                {
                    Log.Debug($"Player {ev.Player.Nickname} dying Event, Type: {ev.DamageHandler.Type} and Suicide: {ev.DamageHandler.IsSuicide}. Is in Config: {CustomItemDamageTypes.Contains(ev.DamageHandler.Type)}");
                    if (CustomItemDamageTypes.Contains(ev.DamageHandler.Type))
                    {
                        Core.CruxifixAbility(ev);
                    }
                }
                else if (!IsAllowed)
                {
                    Log.Debug("UsingCrucifixEvent canceled!"); 
                }
            }
        }

        private void BlockThrowEvents(FlippingCoinEventArgs ev)
        {
            if (Check(ev.Item))
            {
                ev.IsAllowed = false;
                ev.IsAllowed = true;
            }
        }
    }
}