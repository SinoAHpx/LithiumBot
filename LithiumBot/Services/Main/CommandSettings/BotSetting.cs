using System.ComponentModel;
using Spectre.Console.Cli;

namespace LithiumBot.Services.Main.CommandSettings
{
    public class BotSetting : Spectre.Console.Cli.CommandSettings
    {
        [CommandOption("--launch")]
        public bool? Launch { get; set; }

        [CommandOption("--exit")]
        public bool? Exit { get; set; }
    }
}