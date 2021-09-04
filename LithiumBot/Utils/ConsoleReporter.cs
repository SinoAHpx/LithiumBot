using Spectre.Console;

namespace LithiumBot.Utils
{
    public static class ConsoleReporter
    {
        public static void Error(string message)
        {
            AnsiConsole.MarkupLine($"[bold red]ERROR: [/]{message}");
        }

        public static void Success(string message)
        {
            AnsiConsole.MarkupLine($"[bold green]SUCCESS: [/]{message}");
        }

        public static void Info(string message)
        {
            AnsiConsole.MarkupLine($"[bold blue]INFO: [/]{message}");
        }

        public static void Warn(string message)
        {
            AnsiConsole.MarkupLine($"[bold yellow]WARN: [/]{message}");
        }
    }
}