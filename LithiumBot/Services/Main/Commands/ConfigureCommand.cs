using System;
using System.Diagnostics;
using System.IO;
using AHpx.Extensions.IOExtensions;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Services.Main.CommandSettings;
using LithiumBot.Utils;
using LithiumBot.Utils.Main;
using Mirai.Net.Sessions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace LithiumBot.Services.Main.Commands
{
    public class ConfigureCommand : Command<ConfigureSettings>
    {
        public override int Execute(CommandContext context, ConfigureSettings settings)
        {
            settings.Validate();

            ConsoleReporter.Info($"Config path is {MiraiBotUtils.Config}");

            if (settings.New)
            {
                MiraiBotUtils.Config.Delete();
                ConsoleReporter.Warn("Deleted old configuration.");
            }

            if (!MiraiBotUtils.Config.Exists)
            {
                ConsoleReporter.Info("Config is not exist, creating new.");

                if (!MiraiBotUtils.Config.Directory.Exists)
                {
                    ConsoleReporter.Warn("Parent directory of config path is not exist, creating new.");
                    MiraiBotUtils.Config.Directory.Create();
                }

                var (address, verifyKey) = MiraiBotUtils.GetMiraiConfig(settings.ConfigPath);

                var configJson = new
                {
                    VerifyKey = verifyKey,
                    Address = address,
                    settings.QQ,
                    ApiKeys = MiraiBotUtils.GetApiKeysJsonAppendix()
                }.ToJsonString();

                MiraiBotUtils.Config.WriteAllText(configJson);

                ConsoleReporter.Warn($"Write config successfully, [bold]but you need to specify api keys.[/]");

                if (Environment.OSVersion.Platform.ToString().Contains("Win"))
                {
                    if (AnsiConsole.Confirm("Open the config file?"))
                    {
                        Process.Start("explorer.exe", MiraiBotUtils.Config.FullName);
                    }
                }
            }
            else
            {
                ConsoleReporter.Warn("Config already exist, nothing to do.");
            }

            return 0;
        }
    }
}