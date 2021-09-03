using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LithiumBot.Modules;
using LithiumBot.Utils;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Modules;
using Mirai.Net.Sessions;
using Mirai.Net.Utils.Scaffolds;
using Spectre.Console;

namespace LithiumBot
{
    public static class Program
    {
        public static List<ICommandModule> Modules { get; set; }

        public static string ModulesStr { get; set; }

        private static async Task Main(string[] args)
        {
            MiraiBot bot = null;
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Star)
                .StartAsync("Launching...", async ctx =>
                {
                    #region Mirai bot

                    AnsiConsole.MarkupLine("Creating primary MiraiBot instance...");
                    bot = new MiraiBot
                    {
                        Address = "localhost:8080",
                        QQ = "2672886221",
                        VerifyKey = "1145141919810"
                    };

                    await bot.LaunchAsync();
                    AnsiConsole.MarkupLine("MiraiBot created...");


                    #endregion

                    #region Modules

                    ctx.Status("Loading modules....");

                    Modules = CommandScaffold.LoadCommandModules<BasicModule>().ToList();
                    ModulesStr = string.Join("\n",
                        Modules.Where(x => x.IsEnable is not false).Select(x => x.GetType().Name));
                    AnsiConsole.MarkupLine(
                        $"Following modules loaded: {ModulesStr}");

                    #endregion

                    #region Subscribers

                    ctx.Status("Subscribing websocket notifications....");

                    AnsiConsole.MarkupLine("Listening to GroupMessage...");

                    bot.MessageReceived
                        .WhereAndCast<GroupMessageReceiver>()
                        .Subscribe(x =>
                        {
                            x.ExecuteCommandModules(Modules);
                        });

                    #endregion
                });

            while (!AnsiConsole.Confirm("Exit?"))
            {
            }

            bot.Dispose();
        }
    }
}
