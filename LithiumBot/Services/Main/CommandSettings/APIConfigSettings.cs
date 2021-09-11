using Spectre.Console.Cli;

namespace LithiumBot.Services.Main.CommandSettings
{
    public class ApiConfigSettings : ConfigureSettings
    {
        [CommandArgument(0, "<API_NAME>")]
        public string Name { get; set; }

        [CommandArgument(1, "API_KEY")]
        public string Value { get; set; }
    }
}