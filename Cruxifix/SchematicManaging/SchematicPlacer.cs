using System.Collections.Generic;
using System.Data;
using Cruxifix.Extensions;
using Exiled.API.Features;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.API.Features;
using MEC;
using UnityEngine;
using Mirror;
using PlayerRoles;
using Object = UnityEngine.Object;

namespace Cruxifix.SchematicManaging
{
    public class SchematicPlacer
    {
        private readonly Dictionary<Player, SchematicObject> previewSchematics = new();
        private readonly Dictionary<Player, CoroutineHandle> previewCoroutines = new();

        public void PlaceSchematic(Player player, string schematicName)
        {
            if (player == null || !player.IsConnected || player.Role.Type == RoleTypeId.Spectator)
                return;

            try
            {
                if (Physics.Raycast(player.CameraTransform.position, player.CameraTransform.forward, out RaycastHit hitInfo, Plugin.Singleton.Config.CustomItemMaxRange, LayerMask.GetMask("Default", "Terrain")))
                {
                    Quaternion rotation = Quaternion.LookRotation(hitInfo.normal * -1f);

                    var schematic = ObjectSpawner.SpawnSchematic(
                        schematicName,
                        hitInfo.point,
                        rotation,
                        Vector3.one,
                        MapUtils.GetSchematicDataByName(schematicName),
                        true
                    );

                    Events.CustomEvents.InvokeOnPlacedSchematic(
                        new Events.PlacedSchematicEventArgs(player, schematic)
                    );
                    Timing.RunCoroutine(WatchCrucifixRange(schematic, player));
                }
                else
                {
                    player.ShowMeowHint(Plugin.Singleton.Translation.CantPlace);
                }
            }
            catch (System.Exception ex)
            {
                var text = Plugin.Singleton.Translation.ErrorMessage.Replace("{Exception}", ex.GetType().Name).Replace("{ErrorClass}", "PlaceSchematic");
                player.ShowMeowHint(text);
                Log.Error($"[PlaceSchematic] ERROR: {player.Nickname}: {ex}");
            }
        }

        private SchematicObject SpawnPreviewSchematic(Player player, string schematicName)
        {
            SchematicObject schematic = ObjectSpawner.SpawnSchematic(
                schematicName,
                player.Position,
                Quaternion.identity,
                Vector3.one,
                MapUtils.GetSchematicDataByName(schematicName),
                false
            );
            foreach (var prim in schematic.GetComponentsInChildren<PrimitiveObject>())
            {
                var oldPrimColor = prim.Primitive.Color;
                prim.Primitive.Color = new Color(oldPrimColor.r, oldPrimColor.g, oldPrimColor.b, Plugin.Singleton.Config.CustomItemTransparent);
            }

            return schematic;
        }
        
        public void StartPreview(Player player, string schematicName)
        {
            if (player == null || !player.IsConnected || player.Role.Type == RoleTypeId.Spectator)
                return;

            if (previewSchematics.ContainsKey(player))
                StopPreview(player);

            SchematicObject schematic = SpawnPreviewSchematic(player, schematicName);

            Events.CustomEvents.InvokeStartingPlacedSchematicPrewiev(
                new Events.StartingPlacedSchematicPrewievEventArgs(player, schematic)
            );
            
            previewSchematics[player] = schematic;
            HideSchematicFromOthers(player, schematic);
            previewCoroutines[player] = Timing.RunCoroutine(UpdatePreview(player, schematic));
        }

        public void StopPreview(Player player)
        {
            if (previewSchematics.TryGetValue(player, out var schematic))
            {
                Events.CustomEvents.InvokeStoppingPlacedSchematicPrewiev(
                    new Events.StoppingPlacedSchematicPrewievEventArgs(player, schematic)
                );
                schematic.Destroy();
                previewSchematics.Remove(player);
            }

            if (previewCoroutines.TryGetValue(player, out var coroutine))
            {
                Timing.KillCoroutines(coroutine);
                previewCoroutines.Remove(player);
            }
        }

        private IEnumerator<float> WatchCrucifixRange(SchematicObject schematic, Player owner)
        {
            const float checkInterval = 0.5f;
            const float triggerDistance = 25f;
            bool hasTriggered = false;

            while (schematic != null && schematic.isActiveAndEnabled && !hasTriggered)
            {
                foreach (Player player in Player.List)
                {
                    if (player == null || !player.IsConnected || player.Role.Type == RoleTypeId.Spectator || player == owner)
                        continue;

                    float distance = Vector3.Distance(player.Position, schematic.Position);
                    if (distance <= triggerDistance && player.Role.Team == Team.SCPs)
                    {
                        hasTriggered = true;

                        Log.Info($"[Crucifix] Player {player.Nickname} entered within 25m of the crucifix!");

                        Events.CustomEvents.InvokePlayerEnteredCrucifixZone(
                            new Events.PlayerEnteredCrucifixZoneEventArgs(player, owner, schematic)
                        );

                        break;
                    }
                }

                yield return Timing.WaitForSeconds(checkInterval);
            }
        }

        
        private IEnumerator<float> UpdatePreview(Player player, SchematicObject schematic)
        {
            while (player != null && player.IsConnected && previewSchematics.ContainsKey(player))
            {
                if (Physics.Raycast(player.CameraTransform.position, player.CameraTransform.forward, out RaycastHit hitInfo, Plugin.Singleton.Config.CustomItemMaxRange, LayerMask.GetMask("Default", "Terrain")))
                {
                    Quaternion rotation = Quaternion.LookRotation(hitInfo.normal * -1f);
                    schematic.transform.position = hitInfo.point;
                    schematic.transform.rotation = rotation;
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        private void HideSchematicFromOthers(Player targetPlayer, SchematicObject schematic)
        {
            foreach (var identity in schematic.GetComponentsInChildren<NetworkIdentity>())
            {
                foreach (var conn in NetworkServer.connections.Values)
                {
                    if (conn.identity != null && conn.identity.connectionToClient != null)
                    {
                        var playerConn = Player.Get(conn.identity);
                        if (playerConn != null && playerConn != targetPlayer)
                        {
                            ObjectDestroyMessage destroyMessage = new ObjectDestroyMessage
                            {
                                netId = identity.netId
                            };

                            conn.Send(destroyMessage);
                        }
                    }
                }
            }
        }
    }
}
