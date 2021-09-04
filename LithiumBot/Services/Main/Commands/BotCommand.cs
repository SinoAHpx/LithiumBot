using System;
using System.Collections.Generic;
using System.IO;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Services.Main.CommandSettings;
using LithiumBot.Utils;
using LithiumBot.Utils.Main;
using Mirai.Net.Sessions;
using Spectre.Console;
using Spectre.Console.Cli;
using YamlDotNet.Serialization;

namespace LithiumBot.Services.Main.Commands
{
    public class BotCommand : Command<BotSetting>
    {
        public override int Execute(CommandContext context, BotSetting settings)
        {
            if (settings.Exit is true)
            {
                if (AnsiConsole.Confirm("Confirm to exit?"))
                {
                    MiraiBotUtils.MiraiBot?.Dispose();
                    Environment.Exit(0);
                }
            }
            else if(settings.Launch is true)
            {
                AnsiConsole
                    .Status()
                    .Spinner(Spinner.Known.Star)
                    .Start("Launching...", statusContext =>
                    {
                        if (!MiraiBotUtils.Config.Exists)
                        {
                            ConsoleReporter.Error($"There's no config file was found.");
                        }
                        else
                        {
                            var config = MiraiBotUtils.GetLithiumConfig();

                            ConsoleReporter.Info($"Read config, {config.ToJsonString()}.");

                            var bot = new MiraiBot
                            {
                                Address = config.Address,
                                QQ = config.QQ,
                                VerifyKey = config.VerifyKey
                            };

                            ConsoleReporter.Info("Initialized bot.");

                            bot.LaunchAsync();

                            MiraiBotUtils.MiraiBot = bot;

                            ConsoleReporter.Success("Bot successfully launched.");
                        }
                    });
            }
            else
            {
                ConsoleReporter.Error("Nothing to do.");
            }

            return 0;
        }
    }
}