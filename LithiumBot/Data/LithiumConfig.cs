using Newtonsoft.Json.Linq;

namespace LithiumBot.Data
{
    public class LithiumConfig
    {
        public string VerifyKey { get; set; }

        public string QQ { get; set; }

        public string Address { get; set; }

        public JObject ApiKeys { get; set; }
    }
}