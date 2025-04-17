using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items;
using MapEditorReborn.API.Features.Objects;

namespace Cruxifix.Events
{
    public class UsingHolyBibleEventArgs : EventArgs
    {
        public Player Player { get; }
        public SchematicObject Schematic { get; }
        public uint CustomItemID { get; }
        public ItemBase Item { get; }
        public FlippingCoinEventArgs UsingItemEvent { get; }
        public bool IsAllowed { get; }

        public UsingHolyBibleEventArgs(Player player, SchematicObject schematic, uint itemID, ItemBase item, FlippingCoinEventArgs usingItemEvent, bool isAllowed)
        {
            Player = player;
            Schematic = schematic;
            CustomItemID = itemID;
            Item = item;
            UsingItemEvent = usingItemEvent;
            IsAllowed = isAllowed;
        }
    }
}