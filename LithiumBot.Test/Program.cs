using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Data;
using LithiumBot.Utils.Main;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NodeDeserializers;

namespace LithiumBot.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var re = MiraiBotUtils.GetMiraiConfig(
                @"E:\Environments\Bot\config\net.mamoe.mirai-api-http\setting.yml");

            Console.WriteLine(re.ToJsonString());
        }
    }
}
