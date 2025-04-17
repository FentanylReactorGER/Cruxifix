using System;
using System.Buffers;
using Exiled.API.Features;
using MapEditorReborn.API.Features.Objects;

namespace Cruxifix.Events;

public class PlayerEnteredCrucifixZoneEventArgs : EventArgs
{
    public Player Player { get; }
    
    public Player Owner { get; }
    public SchematicObject Schematic { get; }

    public PlayerEnteredCrucifixZoneEventArgs(Player player, Player owner, SchematicObject schematic)
    {
        Player = player;
        Schematic = schematic;
        Owner = owner;
    }
}
