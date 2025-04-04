using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Cruxifix.Configs
{
    public class Translation : ITranslation
    {
        [Description("Custom Item Name")]
        public string CustomItemName { get; set; } = "Cruxifix";
        
        [Description("Custom Item Description")]
        public string CustomItemDescription { get; set; } = "Saves you from Death!";
        
        [Description("Custom Item Use Hint")]
        public string CustomItemUH { get; set; } = "\"For whoever wants to save their life will lose it, but whoever loses their life for me will find it.\"\n— Matthew 16:25";
    }
}