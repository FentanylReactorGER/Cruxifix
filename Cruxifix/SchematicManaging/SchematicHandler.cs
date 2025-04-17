using System.Collections.Generic;
using System.Linq;
using Cruxifix.Configs;
using Cruxifix.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using Light = Exiled.API.Features.Toys.Light;
using MEC;
using UnityEngine;

namespace Cruxifix.SchematicManaging
{
    public class SchematicHandler
    {
        private static readonly Config Config = Plugin.Singleton.Config;

        private readonly List<CustomClasses.SchematicItems> schematicItemsList = Config.CustomSchematicItems;
        
        private readonly Dictionary<Pickup, SchematicObject> ActiveBreads = new();
        
        private Dictionary<Pickup, Light> ActiveLights { get; set; } = new();
        
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
            ActiveLights.Clear();
        }

        private void OnRoundStarted()
        {
            _spawnCoroutine = Timing.RunCoroutine(MonitorPickupsForSchematics());
        }

        private void OnRoundEnded(RoundEndedEventArgs _)
        {
            foreach (var lightsource in ActiveLights.Values)
            {
                lightsource.Destroy();
            }

            ActiveLights.Clear();
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

            if (ActiveBreads.TryGetValue(ev.Pickup, out var bread) && ActiveLights.TryGetValue(ev.Pickup, out var lightsource))
            {
                bread.Destroy();
                lightsource.Destroy();
                ActiveLights.Remove(ev.Pickup);
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
                        var schematicItem = schematicItemsList.FirstOrDefault(item => item.CustomItemId == customItem!.Id);
                        if (schematicItem != null)
                        {
                            pickup.Scale = schematicItem.Scale;

                            var schematic = ObjectSpawner.SpawnSchematic(
                                schematicItem.Name,
                                pickup.Position,
                                Quaternion.Euler(pickup.Rotation.eulerAngles.x, pickup.Rotation.eulerAngles.y, 0),
                                Vector3.one,
                                MapUtils.GetSchematicDataByName(schematicItem.Name),
                                true
                            );

                            Color lightColor = CustomClasses.HexToColor(schematicItem.Color);

                            var light = Light.Create(pickup.Position);
                            light.Color = lightColor;
                            light.Range = 5;
                            light.Intensity = 3;
                            light.ShadowType = LightShadows.Hard;
                            ActiveLights[pickup] = light;
                            
                            Events.CustomEvents.InvokeSchematicItemDropped(
                                new Events.PlayerDropSchematicItemEventArgs(
                                    pickup.PreviousOwner, 
                                    schematic, 
                                    customItem!.Id, 
                                    pickup, 
                                    pickup.Base.ItemId.TypeId
                                )
                            );

                            Timing.RunCoroutine(AttachSchematicToPickup(pickup, schematic, light));
                        }
                    }
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        private IEnumerator<float> AttachSchematicToPickup(Pickup pickup, SchematicObject schematic, Light light)
        {
            if (pickup == null)
                yield break;

            if (schematic == null)
                yield break;

            ActiveBreads[pickup] = schematic;

            while (pickup != null && ActiveBreads.ContainsKey(pickup))
            {
                schematic.transform.position = pickup.Position;
                light.Transform.position = pickup.Position;
                var rotation = pickup.Rotation.eulerAngles;
                schematic.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);

                yield return Timing.WaitForOneFrame;
            }

            if (ActiveBreads.TryGetValue(pickup, out var bread) && ActiveLights.TryGetValue(pickup, out var lightsource))
            {
                bread.Destroy();
                lightsource.Destroy();
                ActiveLights.Remove(pickup);
                ActiveBreads.Remove(pickup);
            }
        }
    }
}
