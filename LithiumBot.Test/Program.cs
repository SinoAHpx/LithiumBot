using System;
using System.Threading.Tasks;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Services.MusicServices;
using LithiumBot.Utils;

namespace LithiumBot.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var track = await LyricService.GetLyricAsync("Time", "Pink Floyd");

            Console.WriteLine(track.ToJsonString());
        }
    }
}
