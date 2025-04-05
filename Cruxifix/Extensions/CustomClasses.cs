using System.Collections.Generic;
using Exiled.API.Features;
using Scp914;

namespace Cruxifix.Extensions;

public class CustomClasses
{
    public class PlayerDangerInfo
    {
        public string DangerType { get; set; }
        public List<Player> RelatedPlayers { get; set; }

        public PlayerDangerInfo(string dangerType, List<Player> relatedPlayers = null)
        {
            DangerType = dangerType;
            RelatedPlayers = relatedPlayers ?? new List<Player>();
        }
    }

    public class CustomItemRecipes
    {
        public ItemType? ItemIn { get; set; }
        public uint? CustomItemInput { get; set; }
        public ItemType? ItemOut { get; set; }
        public uint? CustomItemOutput { get; set; }
        public Scp914KnobSetting KnobSetting { get; set; }
        public int Chance { get; set; } = 100; // Default to 100% chance

        public CustomItemRecipes()
        {
        }

        public CustomItemRecipes(uint customItemInput, ItemType itemOut, Scp914KnobSetting knobSetting,
            int chance = 100)
        {
            CustomItemInput = customItemInput;
            ItemOut = itemOut;
            KnobSetting = knobSetting;
            Chance = chance;
        }

        public CustomItemRecipes(ItemType itemIn, uint customItemOutput, Scp914KnobSetting knobSetting,
            int chance = 100)
        {
            ItemIn = itemIn;
            CustomItemOutput = customItemOutput;
            KnobSetting = knobSetting;
            Chance = chance;
        }

        public CustomItemRecipes(ItemType itemIn, ItemType itemOut, Scp914KnobSetting knobSetting, int chance = 100)
        {
            ItemIn = itemIn;
            ItemOut = itemOut;
            KnobSetting = knobSetting;
            Chance = chance;
        }

        public CustomItemRecipes(uint customItemInput, uint customItemOutput, Scp914KnobSetting knobSetting,
            int chance = 100)
        {
            CustomItemInput = customItemInput;
            CustomItemOutput = customItemOutput;
            KnobSetting = knobSetting;
            Chance = chance;
        }
    }
}