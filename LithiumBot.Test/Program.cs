using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AHpx.Extensions.StringExtensions;
using LithiumBot.Services.MusicServices;
using LithiumBot.Utils;

namespace LithiumBot.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var xmlStr = await File.ReadAllTextAsync(@"C:\Users\ahpx\Desktop\testxml.xml");

            var xml = XDocument.Load(new StringReader(xmlStr));

            var nodes = xml.DescendantNodes().OfType<XElement>();

            Console.WriteLine(nodes.First(x => x.Name.LocalName == "Lyric").Value);
        }
    }
}
