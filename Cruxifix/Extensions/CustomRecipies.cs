using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using Exiled.CustomItems.API.Features;
using Exiled.Events.EventArgs.Scp914;
using SCP914Events = Exiled.Events.Handlers.Scp914;

namespace Cruxifix.Extensions
{
    public class CustomRecipes
    {
        private readonly IReadOnlyList<CustomClasses.CustomItemRecipes> _recipes;
        private readonly float _scp914Time;
        private readonly bool _custom914Enabled;
        private readonly System.Random _random = new();

        public CustomRecipes()
        {
            var config = Plugin.Singleton.Config;
            _custom914Enabled = config.Custom914;

            _recipes = config.CustomItemRecipeDictionary
                .Select(configRecipe => new CustomClasses.CustomItemRecipes
                {
                    ItemIn = configRecipe.ItemIn,
                    ItemOut = configRecipe.ItemOut,
                    CustomItemInput = configRecipe.CustomItemInput,
                    CustomItemOutput = configRecipe.CustomItemOutput,
                    KnobSetting = configRecipe.KnobSetting,
                    Chance = configRecipe.Chance
                })
                .ToList();
        }

        public void SubscribeEvents()
        {
            if (!_custom914Enabled)
                return;

            SCP914Events.UpgradingPickup += OnUpgradingPickup;
            SCP914Events.UpgradingInventoryItem += OnUpgradingInventoryItem;
        }

        public void UnsubscribeEvents()
        {
            if (!_custom914Enabled)
                return;

            SCP914Events.UpgradingPickup -= OnUpgradingPickup;
            SCP914Events.UpgradingInventoryItem -= OnUpgradingInventoryItem;
        }

        private void OnUpgradingInventoryItem(UpgradingInventoryItemEventArgs ev)
        {
            if (ev.Player.CurrentItem is null)
            {
                ev.IsAllowed = true;
                return;
            }

            var recipe = _recipes.FirstOrDefault(r =>
                r.KnobSetting == ev.KnobSetting &&
                r.ItemIn.HasValue &&
                r.ItemIn.Value == ev.Player.CurrentItem.Base.ItemTypeId);

            if (recipe != null && _random.Next(1, 101) <= recipe.Chance)
            {
                ev.Player.CurrentItem.Destroy();
                ApplyRecipeResult(ev.Player, recipe);
            }
        }

        private void OnUpgradingPickup(UpgradingPickupEventArgs ev)
        {
            ev.IsAllowed = true;

            if (CustomItem.TryGet(ev.Pickup, out var customItem))
            {
                var recipe = _recipes.FirstOrDefault(r =>
                    r.CustomItemInput.HasValue &&
                    r.CustomItemInput.Value == customItem.Id &&
                    r.KnobSetting == ev.KnobSetting);

                if (TryProcessRecipe(ev, recipe, ev.OutputPosition))
                    return;
            }

            var normalRecipe = _recipes.FirstOrDefault(r =>
                r.ItemIn.HasValue &&
                r.ItemIn.Value == ev.Pickup.Base.ItemId.TypeId &&
                r.KnobSetting == ev.KnobSetting);

            TryProcessRecipe(ev, normalRecipe, ev.OutputPosition);
        }

        private bool TryProcessRecipe(UpgradingPickupEventArgs ev, CustomClasses.CustomItemRecipes recipe, UnityEngine.Vector3 position)
        {
            if (recipe == null || _random.Next(1, 101) > recipe.Chance)
                return false;

            ev.IsAllowed = false;
            ev.Pickup.Destroy();
            SpawnRecipeResult(recipe, position);
            return true;
        }

        private void ApplyRecipeResult(Player player, CustomClasses.CustomItemRecipes recipe)
        {
            if (recipe.ItemOut.HasValue)
                player.AddItem(recipe.ItemOut.Value);
            else if (recipe.CustomItemOutput.HasValue)
                CustomItem.TryGive(player, recipe.CustomItemOutput.Value);
        }

        private void SpawnRecipeResult(CustomClasses.CustomItemRecipes recipe, UnityEngine.Vector3 position)
        {
            if (recipe.ItemOut.HasValue)
                Pickup.CreateAndSpawn(recipe.ItemOut.Value, position);
            else if (recipe.CustomItemOutput.HasValue)
                CustomItem.TrySpawn(recipe.CustomItemOutput.Value, position, out _);
        }
    }
}
