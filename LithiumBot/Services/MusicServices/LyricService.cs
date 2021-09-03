using System.Threading.Tasks;
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
            var track = await TrackService.GetTrackAsync(trackName, artistName);

            var response = await $"https://api.musixmatch.com/ws/1.1"
                .AppendPathSegment("track.lyrics.get")
                .SetQueryParam("apikey", APISecretsManager.MusixmatchKey)
                .SetQueryParam("format", "json")
                .SetQueryParam("track_id", track.Id)
                .GetStringAsync();

            return response.Fetch("message.body.lyrics.lyrics_body");
        }
    }
}