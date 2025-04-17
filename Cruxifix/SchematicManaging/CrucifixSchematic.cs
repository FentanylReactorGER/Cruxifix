using System;
using System.Collections.Generic;
using System.Linq;
using Cruxifix.Configs;
using Cruxifix.Events;
using Cruxifix.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API.Features.Objects;
using MEC;
using UnityEngine;
using ExiledHandlers = Exiled.Events.Handlers;

namespace Cruxifix.SchematicManaging;

public class CrucifixSchematic
{
    private static readonly CustomTranslations CustomTranslations = Plugin.Singleton.CustomTranslations;
    private static readonly SchematicHolder SchematicHolder = Plugin.Singleton.SchematicHolder;
    private static readonly Core Core = Plugin.Singleton.Core;
    private static readonly Config Config = Plugin.Singleton.Config;
    private static readonly Translation Translation = Plugin.Singleton.Translation;
    
    private readonly Dictionary<Player, SchematicObject> _Placedcrucifx = new();

    public void SubscribeEvents()
    {
        CustomEvents.OnPlacedSchematic += PlacingCrucifix;
        CustomEvents.PlayerEnteredCrucifixZone += InvokeCrucifix;
    }
    
    public void UnsubscribeEvents()
    {
        CustomEvents.OnPlacedSchematic -= PlacingCrucifix;
        CustomEvents.PlayerEnteredCrucifixZone -= InvokeCrucifix;
    }

    private void PlacingCrucifix(object obj, PlacedSchematicEventArgs ev)
    {
        if (!_Placedcrucifx.ContainsKey(ev.Player))
        {
            _Placedcrucifx[ev.Player] = ev.Schematic;
        }
    }

    private void InvokeCrucifix(PlayerEnteredCrucifixZoneEventArgs ev)
    {
        BurnCrucifix(ev, 1);
    }
    
    private void BurnCrucifix(PlayerEnteredCrucifixZoneEventArgs ev, int stage)
    {
        switch (stage)
        {
            case 1:
                Timing.RunCoroutine(BurnCrucifixAnimation(ev.Schematic, 25f, Config.BurningCrucifixColor, 5));
                ev.Owner.ShowMeowHint(Translation.BurnCrucifixHint.Replace("{EnterTeam}", CustomTranslations.GetTeamTranslation(ev.Player.Role.Team)).Replace("{EnterZone}", CustomTranslations.GetZoneTranslation(ev.Player.Zone)));
                var destination = Room.List
                    .Where(r => r.Zone != ev.Player.CurrentRoom.Zone && r.Type != RoomType.Pocket)
                    .OrderBy(_ => Plugin.Random.Next())
                    .FirstOrDefault();
                break;
        }
    }

    private IEnumerator<float> BurnCrucifixAnimation(SchematicObject schematicObject, float prozent, Color newColor, float duration)
    {
        var parts = schematicObject.GetComponentsInChildren<PrimitiveObject>();
        List<PrimitiveObject> objects = new();
        foreach (var part in parts)
        {
            objects.Add(part);
        }
        float partAmmount = objects.Count * prozent / 100f;
        foreach (var part in objects)
        {
            Color oldColor = part.Primitive.Color;
            objects.Remove(part);
            Timing.RunCoroutine(ChangeColorOverTime(part, oldColor, newColor, duration));
            partAmmount--;
            if (partAmmount < 0f)
            {
                break;
            }
            
            yield return Timing.WaitForSeconds(1f);
        }
    }

    private IEnumerator<float> ChangeColorOverTime(PrimitiveObject part, Color from, Color to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            part.Primitive.Color = Color.Lerp(from, to, t);
            yield return Timing.WaitForOneFrame;
        }
        
        part.Primitive.Color = to;
    }
}