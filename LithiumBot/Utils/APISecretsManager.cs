using System;
using System.IO;
using AHpx.Extensions.IOExtensions;
using AHpx.Extensions.JsonExtensions;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Utils.Main;

namespace LithiumBot.Utils
{
    public static class APISecretsManager
    {
        public static string Musixmatch => 
            MiraiBotUtils.Config.ReadAllText()?.Fetch($"ApiKeys.{nameof(Musixmatch)}");
    }
}