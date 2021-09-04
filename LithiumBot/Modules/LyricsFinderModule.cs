using System.Linq;
using System.Threading.Tasks;
using AHpx.Extensions.JsonExtensions;
using AHpx.Extensions.StringExtensions;
using Flurl;
using Flurl.Http;
using LithiumBot.Data;
using LithiumBot.Services.MusicServices;
using LithiumBot.Utils;
using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Data.Modules;
using Mirai.Net.Modules;
using Mirai.Net.Utils.Scaffolds;

namespace LithiumBot.Modules
{
    public class LyricsFinderModule : ICommandModule
    {
        [CommandTrigger("lyric")]
        public async void Execute(MessageReceiverBase receiver, MessageBase executeMessage)
        {
            if (receiver is not GroupMessageReceiver gr) return;
            if (executeMessage is not PlainMessage message) return;

            var trigger = this.GetCommandTrigger();

            var parameters = trigger.ParseCommand(message.Text);

            if (parameters.Count == 0
                || parameters.All(x => x.Key != "track")
                || parameters.All(x => x.Key != "artist"))
            {
                await gr.SendGroupMessageAsync("大约是缺少参数罢。");
                return;
            }

            var lyric = await LyricService.GetLyricAsync(string.Join(" ", parameters["track"]),
                string.Join(" ", parameters["artist"]));

            await gr.SendGroupMessageAsync($"{lyric}\r\nhttp://www.chartlyrics.com/api.aspx");
        }

        public bool? IsEnable { get; set; }
    }
}