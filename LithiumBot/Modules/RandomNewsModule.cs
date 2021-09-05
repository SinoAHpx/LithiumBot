using Mirai.Net.Data.Messages;
using Mirai.Net.Data.Messages.Concretes;
using Mirai.Net.Data.Messages.Receivers;
using Mirai.Net.Data.Modules;
using Mirai.Net.Modules;

namespace LithiumBot.Modules
{
    public class RandomNewsModule : ICommandModule
    {
        [CommandTrigger("news")]
        public async void Execute(MessageReceiverBase r, MessageBase executeMessage)
        {
            if(r is not GroupMessageReceiver receiver) return;
            if (executeMessage is not PlainMessage message) return;

            
        }

        public bool? IsEnable { get; set; }
    }
}