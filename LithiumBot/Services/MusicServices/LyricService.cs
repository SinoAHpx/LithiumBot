using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AHpx.Extensions.JsonExtensions;
using AHpx.Extensions.StringExtensions;
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

            var re = xml.DescendantNodes()
                .OfType<XElement>()
                .First(x => x.Name.LocalName == "Lyric")
                .Value;

            if (re.IsNullOrEmpty())
            {
                var track = await TrackService.GetTrackAsync(trackName, artistName);

                var response1 = await $"https://api.musixmatch.com/ws/1.1"
                    .AppendPathSegment("track.lyrics.get")
                    .SetQueryParam("apikey", APISecretsManager.Musixmatch)
                    .SetQueryParam("format", "json")
                    .SetQueryParam("track_id", track.Id)
                    .GetStringAsync();

                return $"{response1.Fetch("message.body.lyrics.lyrics_body")}\r\n只有30%的歌词。\r\nhttps://api.musixmatch.com";
            }

            return $"{re}\r\nhttp://www.chartlyrics.com/api.aspx";
        }
    }
}