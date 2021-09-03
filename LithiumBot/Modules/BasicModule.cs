using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Data.Modules;
using Mirai.Net.Modules;
using Mirai.Net.Utils.Scaffolds;

namespace LithiumBot.Modules
{
    public class BasicModule : ICommandModule
    {
        [CommandTrigger("help")]
        public async void Execute(MessageReceiverBase r, MessageBase executeMessage)
        {
            if (r is GroupMessageReceiver receiver)
            {
                await receiver.SendGroupMessageAsync("大抵是锂机器人罢。\r\n".Append("https://github.com/SinoAHpx/LithiumBot"));
            }
        }

        public bool? IsEnable { get; set; }
    }
}