using System;
using Exiled.API.Features;
using InventorySystem.Items;
using MapEditorReborn.API.Features.Objects;

namespace Cruxifix.Events
{
    public class PlayerEquippedSchematicItemEventArgs : EventArgs
    {
        public Player Player { get; }
        public SchematicObject Schematic { get; }
        public uint CustomItemID { get; }
        public ItemBase Item { get; }

        public PlayerEquippedSchematicItemEventArgs(Player player, SchematicObject schematic, uint itemID, ItemBase item)
        {
            Player = player;
            Schematic = schematic;
            CustomItemID = itemID;
            Item = item;
        }
    }
}