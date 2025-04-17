using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items;
using MapEditorReborn.API.Features.Objects;

namespace Cruxifix.Events
{
    public class UsingCrucfixEventArgs : EventArgs
    {
        public Player Player { get; }
        public SchematicObject Schematic { get; }
        public uint CustomItemID { get; }
        public ItemBase Item { get; }
        public DyingEventArgs DyingEvent { get; }
        public bool IsAllowed { get; }

        public UsingCrucfixEventArgs(Player player, SchematicObject schematic, uint itemID, ItemBase item, DyingEventArgs dyingEvent, bool isAllowed)
        {
            Player = player;
            Schematic = schematic;
            CustomItemID = itemID;
            Item = item;
            DyingEvent = dyingEvent;
            IsAllowed = isAllowed;
        }
    }
}