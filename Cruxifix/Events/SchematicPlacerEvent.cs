using System;
using Exiled.API.Features;
using InventorySystem.Items;
using MapEditorReborn.API.Features.Objects;

namespace Cruxifix.Events
{
    public class PlacedSchematicEventArgs : EventArgs
    {
        public Player Player { get; }
        public SchematicObject Schematic { get; }

        public PlacedSchematicEventArgs(Player player, SchematicObject schematic)
        {
            Player = player;
            Schematic = schematic;
        }
    }
    
    public class StartingPlacedSchematicPrewievEventArgs : EventArgs
    {
        public Player Player { get; }
        public SchematicObject Schematic { get; }

        public StartingPlacedSchematicPrewievEventArgs(Player player, SchematicObject schematic)
        {
            Player = player;
            Schematic = schematic;
        }
    }
    
    public class StoppingPlacedSchematicPrewievEventArgs : EventArgs
    {
        public Player Player { get; }
        public SchematicObject Schematic { get; }

        public StoppingPlacedSchematicPrewievEventArgs(Player player, SchematicObject schematic)
        {
            Player = player;
            Schematic = schematic;
        }
    }
}