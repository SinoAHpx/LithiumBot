using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Modules;
using LithiumBot.Services.Main.Commands;
using LithiumBot.Utils;
using LithiumBot.Utils.Main;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Modules;
using Mirai.Net.Sessions;
using Mirai.Net.Utils.Scaffolds;
using Spectre.Console;
using Spectre.Console.Cli;

namespace LithiumBot
{
    public static class Program
    {
        public static List<ICommandModule> Modules { get; set; }

        public static string ModulesStr { get; set; }

        private static async Task Main(string[] args)
        {
            AnsiConsole.MarkupLine("[blue]" +
                                   " _      _ _   _     _                 \r\n| |    (_) | | |   (_)                \r\n| |     _| |_| |__  _ _   _ _ __ ___  \r\n| |    | | __| '_ \\| | | | | '_ ` _ \\ \r\n| |____| | |_| | | | | |_| | | | | | |\r\n|______|_|\\__|_| |_|_|\\__,_|_| |_| |_|\r\n" +
                                   "[/]");
            AnsiConsole.MarkupLine("Welcome to [bold green]LithiumBot[/] [bold blue]v1.0[/]");

            var app = new CommandApp();

            app.Configure(configurator =>
            {
                configurator
                    .AddCommand<BotCommand>("bot")
                    .WithDescription("Bot related commands.");
                configurator
                    .AddCommand<ConfigureCommand>("config")
                    .WithDescription("Configuration related commands.");
            });

            while (true)
            {
                Console.Write(">>> ");
                await app.RunAsync(Console.ReadLine()!.Split(" "));
            }
        }
    }
}