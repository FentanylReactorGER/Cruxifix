using System.Collections.Generic;
using Cruxifix.Configs;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using MEC;
using UnityEngine;

namespace Cruxifix.SchematicManaging
{
    public class SchematicHandler
    {
        private static readonly Config Config = Plugin.Singleton.Config;

        private readonly Dictionary<Pickup, SchematicObject> ActiveBreads = new();
        private readonly Dictionary<uint, string> CustomItemSchematics = new()
        {
            { Config.CustomItemID, Config.CustomItemSchematic },
            { Config.BibleCustomItemID, Config.BibleCustomSchematicName }
        };

        private readonly Dictionary<uint, Vector3> CustomItemSchematicsScales = new()
        {
            { Config.CustomItemID, Config.CustomItemScaleItem },
            { Config.BibleCustomItemID, Config.BibleCustomItemScale }
        };

        
        private CoroutineHandle _spawnCoroutine;

        public void SubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
        }

        public void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;
            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;

            if (_spawnCoroutine.IsRunning)
                Timing.KillCoroutines(_spawnCoroutine);

            ActiveBreads.Clear();
        }

        private void OnRoundStarted()
        {
            _spawnCoroutine = Timing.RunCoroutine(MonitorPickupsForSchematics());
        }

        private void OnRoundEnded(RoundEndedEventArgs _)
        {
            foreach (var schematic in ActiveBreads.Values)
            {
                schematic.Destroy();
            }
            ActiveBreads.Clear();
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Pickup == null)
                return;

            if (ActiveBreads.TryGetValue(ev.Pickup, out var bread))
            {
                bread.Destroy();
                ActiveBreads.Remove(ev.Pickup);
            }
        }

        private IEnumerator<float> MonitorPickupsForSchematics()
        {
            while (Round.IsStarted)
            {
                foreach (var pickup in Pickup.List)
                {
                    if (!ActiveBreads.ContainsKey(pickup) && CustomItem.TryGet(pickup, out var customItem))
                    {
                        if (CustomItemSchematics.TryGetValue(customItem!.Id, out var schematicName))
                        {
                            pickup.Scale = CustomItemSchematicsScales[customItem.Id];
                            var schematic = ObjectSpawner.SpawnSchematic(
                                schematicName,
                                pickup.Position,
                                Quaternion.Euler(pickup.Rotation.eulerAngles.x, pickup.Rotation.eulerAngles.y, 0),
                                Vector3.one,
                                MapUtils.GetSchematicDataByName(schematicName),
                                true
                            );
                            Events.CustomEvents.InvokeSchematicItemDropped(
                                new Events.PlayerDropSchematicItemEventArgs(pickup.PreviousOwner, schematic, customItem.Id, pickup, pickup.Base.ItemId.TypeId)
                            );
                            Timing.RunCoroutine(AttachSchematicToPickup(pickup, schematic));
                        }
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> AttachSchematicToPickup(Pickup pickup, SchematicObject schematic)
        {
            if (pickup == null)
                yield break;

            if (schematic == null)
                yield break;

            ActiveBreads[pickup] = schematic;

            while (pickup != null && ActiveBreads.ContainsKey(pickup))
            {
                schematic.transform.position = pickup.Position;
                var rotation = pickup.Rotation.eulerAngles;
                schematic.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);

                yield return Timing.WaitForOneFrame;
            }

            if (ActiveBreads.TryGetValue(pickup, out var bread))
            {
                bread.Destroy();
                ActiveBreads.Remove(pickup);
            }

        }
    }
}
