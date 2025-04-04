using Exiled.API.Features;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using MEC;

namespace Cruxifix;

public static class PlayerHsmManager
{
    public static void ShowMeowHint(this Player player, string text)
    {
        PlayerDisplay playerDisplay = PlayerDisplay.Get(player);

        DynamicHint hint = new()
        {
            Text = text,
            TargetY = Plugin.Singleton.Config.GlobalHintY,
            FontSize = Plugin.Singleton.Config.GlobalHintSize,
            SyncSpeed = HintSyncSpeed.Fast,
        };
        playerDisplay.RemoveHint(hint);
        playerDisplay.AddHint(hint);
        Timing.CallDelayed( Plugin.Singleton.Config.GlobalHintDuration,
            () =>
            {
                playerDisplay.RemoveHint(hint);
            });
    }
}