using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AHpx.Extensions.JsonExtensions;
using Flurl;
using Flurl.Http;
using LithiumBot.Utils;

namespace LithiumBot.Services.MusicServices
{
    public static class LyricService
    {
        public static async Task<string> GetLyricAsync(string trackName, string artistName)
        {
            var response = await "http://api.chartlyrics.com/apiv1.asmx"
                .AppendPathSegment("SearchLyricDirect")
                .SetQueryParam("artist", artistName)
                .SetQueryParam("song", trackName)
                .GetStringAsync();

            var xml = XDocument.Load(new StringReader(response));

            return xml.DescendantNodes()
                .OfType<XElement>()
                .First(x => x.Name.LocalName == "Lyric")
                .Value;
        }
    }
}