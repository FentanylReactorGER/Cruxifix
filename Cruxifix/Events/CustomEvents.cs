using System;

namespace Cruxifix.Events
{
    public static class CustomEvents
    {
        public static event EventHandler<PlayerEquippedSchematicItemEventArgs> OnSchematicItemEquipped;
        
        public static event EventHandler<PlayerDropSchematicItemEventArgs> OnSchematicItemDropped;

        internal static void InvokeSchematicItemEquipped(PlayerEquippedSchematicItemEventArgs args)
        {
            OnSchematicItemEquipped?.Invoke(null, args);
        }
        
        internal static void InvokeSchematicItemDropped(PlayerDropSchematicItemEventArgs args)
        {
            OnSchematicItemDropped?.Invoke(null, args);
        }
    }
}