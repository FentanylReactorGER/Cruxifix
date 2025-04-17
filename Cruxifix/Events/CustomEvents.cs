using System;

namespace Cruxifix.Events
{
    public static class CustomEvents
    {
        public static event EventHandler<PlayerEquippedSchematicItemEventArgs> OnSchematicItemEquipped;
        
        public static event EventHandler<PlayerDropSchematicItemEventArgs> OnSchematicItemDropped;
        
        public static event EventHandler<UsingCrucfixEventArgs> OnUsingCrucifix;

        public static event EventHandler<UsingHolyBibleEventArgs> OnUsingHolyBible;
        
        public static event EventHandler<PlacedSchematicEventArgs> OnPlacedSchematic;
        
        public static event EventHandler<StartingPlacedSchematicPrewievEventArgs> onStartingPlacedSchematicPrewievEventArgs;
        
        public static event EventHandler<StoppingPlacedSchematicPrewievEventArgs> OnStoppingPlacedSchematicPrewievEventArgs;
        
        public static event Action<PlayerEnteredCrucifixZoneEventArgs> PlayerEnteredCrucifixZone;

        public static void InvokePlayerEnteredCrucifixZone(PlayerEnteredCrucifixZoneEventArgs ev) => PlayerEnteredCrucifixZone?.Invoke(ev);
        
        internal static void InvokeSchematicItemEquipped(PlayerEquippedSchematicItemEventArgs args)
        {
            OnSchematicItemEquipped?.Invoke(null, args);
        }
        
        internal static void InvokeSchematicItemDropped(PlayerDropSchematicItemEventArgs args)
        {
            OnSchematicItemDropped?.Invoke(null, args);
        }
        
        internal static void InvokeUsingCrucifix(UsingCrucfixEventArgs args)
        {
            OnUsingCrucifix?.Invoke(null, args);
        }
        internal static void InvokeUsingHolyBible(UsingHolyBibleEventArgs args)
        {
            OnUsingHolyBible?.Invoke(null, args);
        }
        
        internal static void InvokeOnPlacedSchematic(PlacedSchematicEventArgs args)
        {
            OnPlacedSchematic?.Invoke(null, args);
        }
        
        internal static void InvokeStartingPlacedSchematicPrewiev(StartingPlacedSchematicPrewievEventArgs args)
        {
            onStartingPlacedSchematicPrewievEventArgs?.Invoke(null, args);
        }
        
        internal static void InvokeStoppingPlacedSchematicPrewiev(StoppingPlacedSchematicPrewievEventArgs args)
        {
            OnStoppingPlacedSchematicPrewievEventArgs?.Invoke(null, args);
        }
    }
}