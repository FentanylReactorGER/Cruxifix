using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Cruxifix.Configs
{
    public class Translation : ITranslation
    {
        [Description("Custom Item Name")] public string CustomItemName { get; set; } = "Crucifix";

        [Description("Custom Item Description")]
        public string CustomItemDescription { get; set; } = "Saves you from Death!";

        [Description("Custom Item Use Hint")]
        public List<string> CustomItemUH { get; set; } = new List<string>()
        {
            "\"For whoever wants to save their life will lose it, but whoever loses their life for me will find it.\"\n— Matthew 16:25",
            "\"Even though I walk through the valley of the shadow of death, I will fear no evil, for you are with me.\"\n— Psalm 23:4",
            "\"The Lord will fight for you; you need only to be still.\"\n— Exodus 14:14",
            "\"God is our refuge and strength, an ever-present help in trouble.\"\n— Psalm 46:1",
            "\"The name of the Lord is a fortified tower; the righteous run to it and are safe.\"\n— Proverbs 18:10",
            "\"I have told you these things, so that in me you may have peace. In this world you will have trouble. But take heart! I have overcome the world.\"\n— John 16:33",
            "\"He will cover you with his feathers, and under his wings you will find refuge.\"\n— Psalm 91:4",
            "\"When you pass through the waters, I will be with you; and when you pass through the rivers, they will not sweep over you.\"\n— Isaiah 43:2",
            "\"The Lord is my light and my salvation—whom shall I fear?\"\n— Psalm 27:1",
            "\"Do not be afraid of those who kill the body but cannot kill the soul.\"\n— Matthew 10:28",
            "\"The Lord your God goes with you; he will never leave you nor forsake you.\"\n— Deuteronomy 31:6",
            "\"He gives strength to the weary and increases the power of the weak.\"\n— Isaiah 40:29",
            "\"My grace is sufficient for you, for my power is made perfect in weakness.\"\n— 2 Corinthians 12:9",
            "\"Be strong and courageous. Do not be afraid; do not be discouraged.\"\n— Joshua 1:9",
            "\"Cast all your anxiety on him because he cares for you.\"\n— 1 Peter 5:7",
            "\"I sought the Lord, and he answered me; he delivered me from all my fears.\"\n— Psalm 34:4",
            "\"No weapon forged against you will prevail.\"\n— Isaiah 54:17",
            "\"In peace I will lie down and sleep, for you alone, Lord, make me dwell in safety.\"\n— Psalm 4:8",
            "\"Call on me in the day of trouble; I will deliver you.\"\n— Psalm 50:15",
            "\"With God we will gain the victory, and he will trample down our enemies.\"\n— Psalm 60:12",
            "\"Because you are my help, I sing in the shadow of your wings.\"\n— Psalm 63:7",
            "\"You are my hiding place; you will protect me from trouble.\"\n— Psalm 32:7",
            "\"Fear not, for I have redeemed you; I have called you by name, you are mine.\"\n— Isaiah 43:1",
            "\"He will not let your foot slip—he who watches over you will not slumber.\"\n— Psalm 121:3",
            "\"The angel of the Lord encamps around those who fear him, and he delivers them.\"\n— Psalm 34:7",
        };

        [Description("Bible Custom Item Use Hint")]

        public string BibleCustomItemName { get; set; } = "Holy Bible";

        public string BibleCustomItemDescription { get; set; } = "It is indeed Holy!";
        
        public string BibleCustomItemHintUsage { get; set; } = "You can use the Bible to get out of Dangerous Situations! \n Just use the Item!";
        
        public string BibleCustomItemNoDanger { get; set; } = "Theres no danger!";
        
        public string BibleCustomItemShowCooldown { get; set; } = "You got Cooldown for {DurationCooldown} Seconds!";
        
        public string BibleCustomItemCooldown { get; set; } = "<color=red>You didn't held the item Long enough!</color>";
        
        public string BibleCustomItemHoldingHint { get; set; } = "<color=red>You must hold the item for {DurationTime} seconds.</color>!";
        
        [Description("You can Edit the Bible Verses but do NOT Edit the Names: 'PocketDimension, NearbySCP, NearbyEnemy and LowHealth'")]
        
        public Dictionary<string, List<string>> BibleVersesForBibleItem { get; set; } = new()
        {
            { 
                "PocketDimension", 
                new List<string>
                {
                    "Isaiah 55:8-9: 'For my thoughts are not your thoughts, neither are your ways my ways,' declares the Lord. 'As the heavens are higher than the earth, so are my ways higher than your ways and my thoughts than your thoughts.'",
                    "2 Corinthians 5:1: 'For we know that if the earthly tent we live in is destroyed, we have a building from God, an eternal house in heaven, not built by human hands.'",
                    "Ephesians 3:18-19: 'May you have power, together with all the Lord’s holy people, to grasp how wide and long and high and deep is the love of Christ, and to know this love that surpasses knowledge—that you may be filled to the measure of all the fullness of God.'"
                }
            },
            { 
                "NearbySCP", 
                new List<string>
                {
                    "Psalm 91:7: 'A thousand may fall at your side, ten thousand at your right hand, but it will not come near you.'",
                    "Isaiah 54:17: 'No weapon forged against you will prevail, and you will refute every tongue that accuses you. This is the heritage of the servants of the Lord, and this is their vindication from me,’ declares the Lord.'",
                    "Psalm 27:1: 'The Lord is my light and my salvation—whom shall I fear? The Lord is the stronghold of my life—of whom shall I be afraid?'"
                }
            },
            { 
                "NearbyEnemy", 
                new List<string>
                {
                    "Romans 12:19: 'Do not take revenge, my dear friends, but leave room for God’s wrath, for it is written: ‘It is mine to avenge; I will repay,’ says the Lord.'",
                    "Psalm 23:5: 'You prepare a table before me in the presence of my enemies. You anoint my head with oil; my cup overflows.'",
                    "Matthew 5:44: 'But I tell you, love your enemies and pray for those who persecute you.'"
                }
            },
            { 
                "LowHealth", 
                new List<string>
                {
                    "Jeremiah 30:17: 'But I will restore you to health and heal your wounds,’ declares the Lord, ‘because you are called an outcast, Zion for whom no one cares.’'",
                    "Psalm 147:3: 'He heals the brokenhearted and binds up their wounds.'",
                    "Isaiah 53:5: 'But he was pierced for our transgressions, he was crushed for our iniquities; the punishment that brought us peace was on him, and by his wounds we are healed.'"
                }
            }
        };
    }
}