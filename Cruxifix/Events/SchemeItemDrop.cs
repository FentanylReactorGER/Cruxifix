using System;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using InventorySystem.Items;
using MapEditorReborn.API.Features.Objects;

namespace Cruxifix.Events
{
    public class PlayerDropSchematicItemEventArgs : EventArgs
    {
        public Player Player { get; }
        public SchematicObject Schematic { get; }
        public uint CustomItemID { get; }
        public Pickup Pickup { get; }
        
        public ItemType Item { get; }

        public PlayerDropSchematicItemEventArgs(Player player, SchematicObject schematic, uint itemID, Pickup pickup, ItemType item)
        {
            Player = player;
            Schematic = schematic;
            CustomItemID = itemID;
            Pickup = pickup;
            Item = item;
        }
    }
}