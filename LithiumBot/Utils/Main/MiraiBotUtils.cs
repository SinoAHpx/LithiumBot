using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AHpx.Extensions.IOExtensions;
using AHpx.Extensions.JsonExtensions;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Data;
using Mirai.Net.Sessions;
using YamlDotNet.Serialization;

namespace LithiumBot.Utils.Main
{
    public static class MiraiBotUtils
    {
        /// <summary>
        /// item1 = address, item2 = verify key
        /// </summary>
        /// <param name="configPath"></param>
        /// <returns></returns>
        public static (string, string) GetMiraiConfig(string configPath)
        {
            var yamlLines = File.ReadAllLines(configPath).ToList();

            foreach (var line in yamlLines.ToList())
            {
                try
                {
                    _ = new Deserializer().Deserialize<Dictionary<string, string>>(line);
                }
                catch
                {
                    yamlLines.Remove(line);
                }
            }

            var yamlStr = string.Join(Environment.NewLine, yamlLines);

            var serializer = new SerializerBuilder().JsonCompatible().Build();

            var json = serializer.Serialize(new Deserializer().Deserialize(new StringReader(yamlStr))!);

            return ($"{json.Fetch("adapterSettings.http.host")}:{json.Fetch("adapterSettings.http.port")}",
                json.Fetch("verifyKey"));
        }

        public static LithiumConfig GetLithiumConfig()
        {
            var json = Config.ReadAllText();

            return new LithiumConfig
            {
                Address = json.Fetch("Address"),
                QQ = json.Fetch("QQ"),
                VerifyKey = json.Fetch("VerifyKey")
            };
        }

        public static MiraiBot MiraiBot { get; set; }

        public static FileInfo Config =>
            new($"{Directory.GetCurrentDirectory()}/LithiumBot/LithiumConfig.json".ConstructPath());
    }
}