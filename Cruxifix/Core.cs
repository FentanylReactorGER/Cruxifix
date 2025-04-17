using System;
using System.Collections.Generic;
using System.Linq;
using Cruxifix.Configs;
using Cruxifix.Extensions;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using LabApi.Events.Arguments.PlayerEvents;
using MapEditorReborn.API.Features.Objects;
using MEC;
using PlayerRoles;
using PluginAPI.Roles;
using UnityEngine;
using Player = Exiled.API.Features.Player;
using Random = UnityEngine.Random;

namespace Cruxifix
{
    public class Core
    {
        public Dictionary<Player, float> _bibleCooldowns = new Dictionary<Player, float>();
        private static readonly Config Config = Plugin.Singleton.Config;
        private static readonly Translation Translation = Plugin.Singleton.Translation;
        private Animator BibleAnimator { get; set; }

        public void UseBible(FlippingCoinEventArgs ev, SchematicObject bible)
        {
            if (_bibleCooldowns.ContainsKey(ev.Player))
            {
                ev.Player.ShowMeowHint(Translation.BibleCustomItemShowCooldown.Replace("{DurationCooldown}", _bibleCooldowns[ev.Player].ToString("0")));
                return;
            }
            Timing.RunCoroutine(WaitHoldingCheck(ev, bible));
        }

        private CustomClasses.PlayerDangerInfo CheckDangerForBible(Player player)
        {
            if (player.CurrentRoom.Type == RoomType.Pocket)
                return new CustomClasses.PlayerDangerInfo("PocketDimension");

            var nearbyScps = Player.List.Where(p =>
                p != player && !p.IsDead &&
                p.Role.Team == Team.SCPs &&
                Vector3.Distance(p.Position, player.Position) <= Config.BibleCustomItemRange).ToList();

            if (nearbyScps.Count > 0)
                return new CustomClasses.PlayerDangerInfo("NearbySCP", nearbyScps);

            var nearbyEnemies = Player.List.Where(p =>
                p != player && !p.IsDead &&
                p.ReferenceHub.roleManager.CurrentRole.Team != player.ReferenceHub.roleManager.CurrentRole.Team &&
                Vector3.Distance(p.Position, player.Position) <= Config.BibleCustomItemRange ).ToList();

            if (nearbyEnemies.Count > 0)
                return new CustomClasses.PlayerDangerInfo("NearbyEnemy", nearbyEnemies);
            
            if (player.ActiveEffects.Any(e => Enum.TryParse<EffectType>(e.name, out var effectType) && Config.BibleHealEffectList.Contains(effectType)))
            {
                return new CustomClasses.PlayerDangerInfo("DeadlyEffects");
            }
            

            if (player.Health <= 25)
                return new CustomClasses.PlayerDangerInfo("LowHealth");

            return null;
        }

        private IEnumerator<float> CooldownTick()
        {
            while (_bibleCooldowns.Count > 0)
            {
                var playersToRemove = new List<Player>();

                var keys = _bibleCooldowns.Keys.ToList(); // To safely iterate while modifying dictionary

                foreach (var player in keys)
                {
                    _bibleCooldowns[player] -= 1f;

                    if (_bibleCooldowns[player] <= 0f)
                    {
                        playersToRemove.Add(player);
                    }
                }

                foreach (var player in playersToRemove)
                {
                    _bibleCooldowns.Remove(player);
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }
        
        public IEnumerator<float> WaitHoldingCheck(FlippingCoinEventArgs ev, SchematicObject bible)
        {
            float timer = 0f;

            while (timer < 15f)
            {
                if (!CustomItem.TryGet(ev.Player.CurrentItem, out CustomItem custom) || custom?.Id != Config.BibleCustomItemID)
                {
                    ev.Player.ShowMeowHint(Translation.BibleCustomItemCooldown);
                    yield break;
                }

                if (timer > 9f)
                {
                    PlayAnimation(bible, Config.BibleCustomAnimationName, Config.BibleCustomAnimatorName);
                }
                
                timer += 1f;
                ev.Player.ShowHint(Translation.BibleCustomItemHoldingHint.Replace("{DurationTime}", timer.ToString("0.00")), 1f);
                yield return Timing.WaitForSeconds(1f);
            }
            var dangerInfo = CheckDangerForBible(ev.Player);
            if (dangerInfo != null)
            {
                AudioPlayerManager.PlaySpatialAudio(ev.Player, Config.MaxClipRange, Config.ClipVolume, Config.ClipDuration, Config.ClipNameBible);
                _bibleCooldowns.Add(ev.Player, Random.Range(20f, 90f));
                Timing.RunCoroutine(CooldownTick());
                switch (dangerInfo.DangerType)
                {
                    case "PocketDimension":
                        var safeRoom = Room.List.Where(r => r.Type != RoomType.Pocket && r.Players.Any(p => p.Role.Team != Team.SCPs)).OrderBy(_ => Plugin.Random.Next()).FirstOrDefault();

                        if (safeRoom != null)
                        {
                            ev.Player.Position = safeRoom.Position + Vector3.up;
                            foreach (EffectType effect in Config.BibleCustomItemEffects)
                            {
                                ev.Player.EnableEffect(effect, Config.BibleCustomEffectDur);
                            }
                            var Verse = Translation.BibleVersesForBibleItem["PocketDimension"];
                            ev.Player.ShowMeowHint(Verse.GetRandomValue());
                            
                        }

                        break;

                    case "NearbySCP":
                        foreach (var scp in dangerInfo.RelatedPlayers)
                        {
                            var destination = Room.List
                                .Where(r => r.Zone != ev.Player.CurrentRoom.Zone && r.Type != RoomType.Pocket)
                                .OrderBy(_ => Plugin.Random.Next())
                                .FirstOrDefault();

                            if (destination != null)
                            {
                                scp.Position = destination.Position + Vector3.up;
                                foreach (EffectType effect in Config.BibleCustomItemEffects)
                                {
                                    scp.EnableEffect(effect, Config.BibleCustomEffectDur);
                                }
                            }
                        }
                        var VerseNearSCP = Translation.BibleVersesForBibleItem["NearbySCP"];
                        ev.Player.ShowMeowHint(VerseNearSCP.GetRandomValue());
                        break;

                    case "NearbyEnemy":
                        foreach (var enemy in dangerInfo.RelatedPlayers)
                        {
                            var destination = Room.List
                                .Where(r => r.Zone != ev.Player.CurrentRoom.Zone && r.Type != RoomType.Pocket)
                                .OrderBy(_ => Plugin.Random.Next())
                                .FirstOrDefault();

                            if (destination != null)
                            {
                                enemy.Position = destination.Position + Vector3.up;
                                foreach (EffectType effect in Config.BibleCustomItemEffects)
                                {
                                    enemy.EnableEffect(effect, Config.BibleCustomEffectDur);
                                }
                            }
                        }
                        var VerseNearEnm = Translation.BibleVersesForBibleItem["NearbyEnemy"];
                        ev.Player.ShowMeowHint(VerseNearEnm.GetRandomValue());
                        break;

                    case "LowHealth":
                        ev.Player.Heal(100);
                        var VerseLowHealth = Translation.BibleVersesForBibleItem["LowHealth"];
                        ev.Player.ShowMeowHint(VerseLowHealth.GetRandomValue());
                        foreach (EffectType effect in Config.BibleCustomItemEffects)
                        {
                            ev.Player.EnableEffect(effect, Config.BibleCustomEffectDur);
                        }
                        break;
                    
                    case "DeadlyEffects":
                        ev.Player.Heal(100);
                        foreach (EffectType effect in Config.BibleCustomItemEffects)
                        {
                            ev.Player.EnableEffect(effect, Config.BibleCustomEffectDur);
                        }
                        var VerseDeadlyEffects = Translation.BibleVersesForBibleItem["LowHealth"];
                        ev.Player.ShowMeowHint(VerseDeadlyEffects.GetRandomValue());
                        ev.Player.DisableAllEffects();
                        break;
                }
            }
            else
            {
                ev.Player.ShowMeowHint(Translation.BibleCustomItemNoDanger);
            }
        }

        public void CruxifixAbility(DyingEventArgs ev)
        {
            ev.IsAllowed = false;
            ev.Player.ShowMeowHint(Translation.CustomItemUH.GetRandomValue());
            Plugin.Singleton.SchematicHolder.DestroyHeld(ev.Player);
            ev.Player.CurrentItem.Destroy();
            ev.Player.DisableAllEffects();
            ApplyCrucifixEffects(ev.Player);
            HealOverTime(ev.Player, Config.CustomItemHealDur, ev);
            AudioPlayerManager.PlaySpatialAudio(ev.Player, Config.MaxClipRange, Config.ClipVolume, Config.ClipDuration, Config.ClipName);
        }

        private void ApplyCrucifixEffects(Player player)
        {
            foreach (EffectType effect in Config.CustomItemEffects)
            {
                player.EnableEffect(effect, Config.CustomItemHealDur);
            }
        }

        public void PlayAnimation(SchematicObject schematic, string animationName, string animatorName)
        {
            foreach (Animator animator in schematic.GetComponentsInChildren<Animator>())
            {
                if (animator.name == animatorName)
                {
                    BibleAnimator = animator;
                    BibleAnimator.Play(animationName);
                }
            }
        }
        
        private void HealOverTime(Player player, float totalDuration, DyingEventArgs ev)
        {
            if (player == null || !player.IsAlive || totalDuration <= 0f)
                return;

            float maxHealth = player.MaxHealth;
            float currentHealth = player.Health;
            float missingHealth = maxHealth - currentHealth;

            if (missingHealth <= 0)
                return;

            float healPerSecond = missingHealth / totalDuration;

            Timing.RunCoroutine(HealCoroutine(player, healPerSecond, totalDuration, ev));
        }

        private IEnumerator<float> HealCoroutine(Player player, float healRate, float duration, DyingEventArgs ev)
        {
            ev.Player.IsGodModeEnabled = true;
            float elapsed = 0f;
            while (elapsed < duration && player.IsAlive)
            {
                player.Health += healRate * Time.deltaTime;
                player.AddAhp(healRate * Time.deltaTime, 2.6f, 0.8f);
                elapsed += Time.deltaTime;
                if (Round.IsEnded)
                {
                    ev.IsAllowed = true;
                    yield break;
                }
                yield return Timing.WaitForOneFrame;
            }
            ev.IsAllowed = true;
            ev.Player.IsGodModeEnabled = false;
            if (player.IsAlive && player.Health > player.MaxHealth)
                player.Health = player.MaxHealth;
        }
    }
}