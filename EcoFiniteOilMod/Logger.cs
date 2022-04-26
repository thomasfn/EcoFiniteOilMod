using System;

namespace Eco.Mods.FiniteOil
{
    using Shared.Localization;
    using Shared.Utils;

    public static class Logger
    {
        public static void Debug(string message)
        {
            Log.Write(new LocString("[FiniteOil] DEBUG: " + message + "\n"));
        }

        public static void Info(string message)
        {
            Log.Write(new LocString("[FiniteOil] " + message + "\n"));
        }

        public static void Error(string message)
        {
            Log.Write(new LocString("[FiniteOil] ERROR: " + message + "\n"));
        }
    }
}