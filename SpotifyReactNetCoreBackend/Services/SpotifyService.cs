using SpotifyReactNetCoreBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace SpotifyReactNetCoreBackend.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"browse/new-releases?country={countryCode}&limit={limit}");
     
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetNewReleaseResult>(responseStream);
            var item = responseObject.albums.items;
            return responseObject?.albums?.items.Select(i => new Release
            {
                Name = i.name,
                Date = i.release_date,
                ImageUrl = i.images.FirstOrDefault().url,
                Link = i.external_urls.spotify,
                Artists = string.Join(",", i.artists.Select(i => i.name))
            }
            );
        }

        public async Task<string> GetUserID(string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "me");

            request.Headers.Add("Authorization", "Bearer " + accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetUser>(responseStream);
            var UserId = responseObject.id;
            return UserId;
        }

        public async Task<IEnumerable<UserPlaylists>> GetUserPlaylist(string accessToken)
        //image,musica, artista
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "me/playlists");
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetUserPlaylist>(responseStream);
            var item = responseObject.items;
            return responseObject?.items.Select(i => new UserPlaylists
            {
                Name = i.name,
                URL = i.external_urls.spotify,
                ImageUrl = i.images.FirstOrDefault().url,
                Description = i.description,
                PlaylistID = i.id
            }
            );
        }
        public async Task<IEnumerable<RecentlyPlayedTracks>> GetRecentlyPlayedTracks(string accessToken)
        //image,musica, artista
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "me/player/recently-played");

            request.Headers.Add("Authorization", "Bearer " + accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetRecentlyPlayedTracksResult>(responseStream);
            var item = responseObject.items;
            return responseObject?.items.Select(i => new RecentlyPlayedTracks
            {
                Name = i.track.name,
                Album = i.track.album.name,
                Album_Release_date = i.track.album.release_date,
                ImageUrl = i.track.album.images.FirstOrDefault().url,
                Url = i.track.external_urls.spotify,
                Artists = string.Join(",", i.track.artists.Select(i => i.name))
            }
            );
        }

        public async Task<CurrentlyPlayingTrack> GetCurrentlyPlayingTrack(string accessToken)
        //image,musica, artista
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "me/player/currently-playing");

            request.Headers.Add("Authorization", "Bearer " + accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetCurrentlyPlayingTrackResult>(responseStream);
            var item = responseObject.item;
            var currentlytrack = new CurrentlyPlayingTrack
            {
                Name = item.name,
                Album = item.album.name,
                ImageUrl = item.album.images.FirstOrDefault().url,
                Url = item.external_urls.spotify,
                Artists = string.Join(",", item.artists.Select(i => i.name))
            };
            return currentlytrack;
        }

        public async Task<IEnumerable<FollowedArtists>> GetFollowedArtists(string accessToken, int limit)
        //image,musica, artista
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"me/following?type=artist&limit={limit}");

            request.Headers.Add("Authorization", "Bearer " + accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetFollowedArtistsResult>(responseStream);
            var item = responseObject.artists.items;
            return responseObject?.artists?.items.Select(i => new FollowedArtists
            {
                Name = i.name,
                ImageUrl = i.images.FirstOrDefault().url,
                Followers  = i.followers.total,
                Popularity = i.popularity
            });
        }

        
        public async Task<IEnumerable<PlaylistItems>> GetPlaylistTracks(string accessToken, string idplaylist)
        //image,musica, artista
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"playlists/{idplaylist}/tracks");

            request.Headers.Add("Authorization", "Bearer " + accessToken);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetPlaylistItemsResult>(responseStream);
            var item = responseObject.items;
            return responseObject?.items.Select(i => new PlaylistItems
            {
                NameTrack = i.track.name,
                ImageUrl = i.track.album.images.FirstOrDefault().url,
                ArtistTrack = string.Join(",", i.track.artists.Select(i => i.name)),
                AlbumTrack = i.track.album.name
            });
        }
    }
}
