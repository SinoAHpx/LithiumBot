using System;
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
    public static class TrackService
    {
        /// <summary>
        /// 根据名称搜索歌曲
        /// </summary>
        /// <param name="trackName"></param>
        /// <param name="artistName"></param>
        /// <returns></returns>
        public static async Task<TrackInfo> GetTrackAsync(string trackName, string artistName)
        {
            var artist = await ArtistService.GetArtistInfoAsync(artistName);

            var response = await $"https://api.musixmatch.com/ws/1.1"
                .AppendPathSegment("track.search")
                .SetQueryParam("apikey", APISecretsManager.MusixmatchKey)
                .SetQueryParam("format", "json")
                .SetQueryParam("q_track", trackName)
                .SetQueryParam("f_artist_id", artist.Id)
                .GetStringAsync();

            var tracks = response.Fetch("message.body.track_list").ToJArray();
            var track = tracks.First().FetchJToken("track");

            return new TrackInfo
            {
                Name = track.Fetch("track_name"),
                Id = track.Fetch("track_id"),
                AlbumId = track.Fetch("album_id"),
                AlbumName = track.Fetch("album_name"),
                ArtistId = track.Fetch("artist_id"),
                ArtistName = track.Fetch("artist_name"),
            };
        }
    }
}