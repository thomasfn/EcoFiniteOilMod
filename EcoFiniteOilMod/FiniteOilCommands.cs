using System;

namespace Eco.Mods.FiniteOil
{
    using Gameplay.Players;
    using Gameplay.Systems.Messaging.Chat.Commands;

    [ChatCommandHandler]
    public static class FiniteOilCommands
    {
        [ChatCommand("FiniteOil", ChatAuthorizationLevel.User)]
        public static void FiniteOil() { }

        [ChatSubCommand("FiniteOil", "Shows the rate of oilfield decay.", ChatAuthorizationLevel.User)]
        public static void Rate(User user)
        {
            var rate = FiniteOilPlugin.Obj.Config.ExtractRate;
            user.MsgLoc($"Oil decay is currently set to {rate:N} ({rate * 100.0f:N}%) of oilfield density per petroleum extracted.");
        }
    }
}
