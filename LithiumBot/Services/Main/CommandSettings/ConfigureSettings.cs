using System.ComponentModel;
using System.IO;
using AHpx.Extensions.StringExtensions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace LithiumBot.Services.Main.CommandSettings
{
    public class ConfigureSettings : Spectre.Console.Cli.CommandSettings
    {
        [CommandArgument(0, "<CONFIG>")]
        public string ConfigPath { get; set; }

        [CommandArgument(1, "<QQ>")]
        public string QQ { get; set; }

        [CommandOption("--new")]
        [DefaultValue(false)]
        public bool New { get; set; }

        public override ValidationResult Validate()
        {
            if (!File.Exists(ConfigPath))
            {
                return ValidationResult.Error("Mirai config file does not exist.");
            }

            if (!QQ.IsInteger())
            {
                return ValidationResult.Error("Invalid QQ number.");
            }

            return ValidationResult.Success();
        }
    }
}