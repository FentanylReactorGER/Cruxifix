using System.Collections.Generic;
using Cruxifix.Configs;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.API.Features.Serializable;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace Cruxifix.SchematicManaging
{
    public class SchematicHolder
    {
        private static readonly Config Config = Plugin.Singleton.Config;

        private readonly Vector3 schematicScale;
        private readonly Vector3 offset;
        private readonly Quaternion initialRotation;

        public Dictionary<uint, string> CustomItemSchematics { get; set; } = new()
        {
            { Config.CustomItemID, Config.CustomItemSchematic },
            { Config.BibleCustomItemID, Config.BibleCustomSchematicName }
        };

        public Dictionary<Player, SchematicObject> activeSchematics = new();
        private readonly Dictionary<Player, CoroutineHandle> followCoroutines = new();

        public SchematicHolder(Vector3 scale, Vector3 offset, Quaternion rotation)
        {
            schematicScale = scale;
            this.offset = offset;
            initialRotation = rotation;
        }

        public void OnChangedItem(ChangedItemEventArgs ev)
        {
            if (activeSchematics.TryGetValue(ev.Player, out var existingSchematic))
            {
                existingSchematic.Destroy();
                activeSchematics.Remove(ev.Player);
            }

            if (followCoroutines.TryGetValue(ev.Player, out var handle))
            {
                Timing.KillCoroutines(handle);
                followCoroutines.Remove(ev.Player);
            }

            if (ev.Item != null && CustomItem.TryGet(ev.Item, out var item) && CustomItemSchematics.TryGetValue(item.Id, out var schematicName))
            {
                var schematic = ObjectSpawner.SpawnSchematic(
                    schematicName,
                    ev.Player.Position,
                    null,
                    schematicScale,
                    MapUtils.GetSchematicDataByName(schematicName)
                );

                activeSchematics[ev.Player] = schematic;
                followCoroutines[ev.Player] = Timing.RunCoroutine(FollowPlayer(ev.Player, schematic));
                
                Events.CustomEvents.InvokeSchematicItemEquipped(
                    new Events.PlayerEquippedSchematicItemEventArgs(ev.Player, schematic, item.Id, ev.Item.Base)
                );
            }
        }
        
        public void DestroyHeld(Player player)
        {
            if (activeSchematics.TryGetValue(player, out var schematic))
            {
                schematic.Destroy();
                activeSchematics.Remove(player);
            }

            if (followCoroutines.TryGetValue(player, out var handle))
            {
                Timing.KillCoroutines(handle);
                followCoroutines.Remove(player);
            }
        }

        public void DestroyAll()
        {
            foreach (var kvp in activeSchematics)
                kvp.Value.Destroy();

            foreach (var handle in followCoroutines.Values)
                Timing.KillCoroutines(handle);

            activeSchematics.Clear();
            followCoroutines.Clear();
        }

        public SchematicObject GetHeldSchematic(Player player)
        {
            return activeSchematics.TryGetValue(player, out var schematic) ? schematic : null;
        }

        public IEnumerator<float> FollowPlayer(Player player, SchematicObject schematic)
        {
            while (!player.IsDead && player.Role.Team != Team.Dead && player.CurrentItem != null &&
                   CustomItem.TryGet(player.CurrentItem, out var item) &&
                   CustomItemSchematics.ContainsKey(item!.Id) &&
                   Round.InProgress)
            {
                Quaternion playerRotation = Quaternion.Euler(0, player.Rotation.eulerAngles.y, 0);
                Vector3 worldOffset = playerRotation * offset;

                schematic.Position = Vector3.Lerp(schematic.Position, player.Position + worldOffset, 0.5f);
                schematic.Rotation = Quaternion.Slerp(schematic.Rotation, playerRotation * initialRotation, 0.5f);

                yield return Timing.WaitForOneFrame;
            }

            DestroyHeld(player);
        }
    }
}