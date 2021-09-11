using System;
using AHpx.Extensions.IOExtensions;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Services.Main.CommandSettings;
using LithiumBot.Utils.Main;
using Spectre.Console;
using Spectre.Console.Cli;

namespace LithiumBot.Services.Main.Commands
{
    public class ApiConfigCommand : Command<ApiConfigSettings>
    {
        public override int Execute(CommandContext context, ApiConfigSettings settings)
        {
            try
            {
                var json = MiraiBotUtils.Config.ReadAllText().ToJObject();

                json["ApiKeys"]![settings.Name] = settings.Value;

                MiraiBotUtils.Config.WriteAllText(json.ToString());
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);

                return -1;
            }

            return 0;
        }
    }
}