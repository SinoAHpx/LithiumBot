using System.Linq;
using System.Threading.Tasks;
using AHpx.Extensions.JsonExtensions;
using AHpx.Extensions.StringExtensions;
using Flurl;
using Flurl.Http;
using LithiumBot.Data;
using LithiumBot.Utils;

namespace LithiumBot.Services.MusicServices
{
    public static class ArtistService
    {
        /// <summary>
        /// 根据名称获取artist
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        public static async Task<ArtistInfo> GetArtistInfoAsync(string artistName)
        {
            var response = await $"https://api.musixmatch.com/ws/1.1"
                .AppendPathSegment("artist.search")
                .SetQueryParam("apikey", APISecretsManager.Musixmatch)
                .SetQueryParam("format", "json")
                .SetQueryParam("q_artist", artistName)
                .GetStringAsync();

            var artists = response.Fetch("message.body.artist_list").ToJArray();

            var artist = artists.First().FetchJToken("artist");

            return new ArtistInfo
            {
                Id = artist.Fetch("artist_id"),
                Name = artist.Fetch("artist_name"),
                Country = artist.Fetch("artist_country"),
                BeginYear = artist.Fetch("begin_date_year"),
                EndYear = artist.Fetch("end_date_year")
            };
        }
    }
}