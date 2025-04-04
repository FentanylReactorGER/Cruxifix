using System.Collections.Generic;
using Cruxifix.Configs;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace Cruxifix
{
    public class Core
    {
        private static readonly Config Config = Plugin.Singleton.Config;
        private static readonly Translation Translation = Plugin.Singleton.Translation;
        
        public void CruxifixAbility(DyingEventArgs ev)
        {
            ev.IsAllowed = false;
            ev.Player.ShowMeowHint(Translation.CustomItemUH);
            Plugin.Singleton.SchematicHolder.DestroyHeld();
            ev.Player.CurrentItem.Destroy();
            HealOverTime(ev.Player, Config.CustomItemHealDur, ev);
            AudioPlayerManager.PlaySpatialAudio(ev.Player, Config.MaxClipRange, Config.ClipVolume, Config.ClipDuration);
        }

        public void HealOverTime(Player player, float totalDuration, DyingEventArgs ev)
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
            float elapsed = 0f;
            while (elapsed < duration && player.IsAlive)
            {
                player.Health += healRate * Time.deltaTime;
                elapsed += Time.deltaTime;
                if (Round.IsEnded)
                {
                    ev.IsAllowed = true;
                    yield break;
                }
                yield return Timing.WaitForOneFrame;
            }
            ev.IsAllowed = true;
            if (player.IsAlive && player.Health > player.MaxHealth)
                player.Health = player.MaxHealth;
        }
    }
}