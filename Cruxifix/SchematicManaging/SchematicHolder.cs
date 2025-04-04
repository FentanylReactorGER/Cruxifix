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
        public SchematicObject HeldSchematic { get; private set; }
        private static readonly Config Config = Plugin.Singleton.Config;

        private readonly Vector3 schematicScale;
        private readonly Vector3 offset;
        private readonly Quaternion initialRotation;

        private CoroutineHandle floatingCoroutine;

        public Dictionary<uint, string> CustomItemSchematics { get; set; } = new()
        {
            { Config.CustomItemID, Config.CustomItemSchematic },
        };

        public SchematicHolder(Vector3 scale, Vector3 offset, Quaternion rotation)
        {
            schematicScale = scale;
            this.offset = offset;
            initialRotation = rotation;
        }

        public void OnChangedItem(ChangedItemEventArgs ev)
        {
            if (HeldSchematic != null)
            {
                HeldSchematic.Destroy();
                HeldSchematic = null;
            }

            if (ev.Item != null && CustomItem.TryGet(ev.Item, out var item) && CustomItemSchematics.TryGetValue(item!.Id, out var schematicName))
            {
                HeldSchematic = ObjectSpawner.SpawnSchematic(
                    schematicName,
                    ev.Player.Position,
                    null,
                    schematicScale,
                    MapUtils.GetSchematicDataByName(schematicName)
                );

                floatingCoroutine = Timing.RunCoroutine(FollowPlayer(ev.Player, HeldSchematic));
            }
        }

        public void DestroyHeld()
        {
            if (HeldSchematic != null)
            {
                HeldSchematic.Destroy();
                HeldSchematic = null;
            }

            if (floatingCoroutine.IsRunning)
                Timing.KillCoroutines(floatingCoroutine);
        }

        private IEnumerator<float> FollowPlayer(Player player, SchematicObject schematic)
        {
            if (schematic == null)
                yield break;

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

            DestroyHeld();
        }
    }
}
