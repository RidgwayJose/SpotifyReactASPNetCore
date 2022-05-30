using SpotifyReactNetCoreBackend.Models;
using SpotifyReactNetCoreBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace SpotifyReactNetCoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {

        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        //private readonly ISpotifyService _spotifyService;

        public HomeController(
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration)
            //ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
           // _spotifyService = spotifyService;;
        }
        static string SaveCode { set; get; }
        static string SaveToken { set; get; }

        public string Login()
        {
            SaveToken = null;
            var state = 12345;
            var endpoint = "https://accounts.spotify.com/authorize";
            var clientID = _configuration["Spotify:ClientId"];
            var redirectUri = _configuration["Spotify:Redirect_URI"];
            string[] scopes = {Scopes.UserReadEmail,
                               Scopes.UserReadPrivate,
                               Scopes.UserReadCurrentlyPlaying,
                               Scopes.UserReadRecentlyPlayed,
                               Scopes.UserReadPlaybackState,
                               Scopes.UserTopRead,
                               Scopes.UserModifyPlaybackState,
                               Scopes.UserLibraryRead,
                               Scopes.PlaylistReadPrivate};


            var loginURL = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                $"{endpoint}?client_id={clientID}" +
                $"&response_type=code" +
                $"&redirect_uri={redirectUri}" +
                $"&state={state}" +
                $"&scope={string.Join("%20", scopes)}" +
                $"&show_dialog=true"));
            
            string strJson = JsonSerializer.Serialize(loginURL);
            return strJson;
        }
        public async Task<IActionResult> GetToken()
        {
            var newLogin = await _spotifyAccountService.TokenRequest(
                HttpContext.Request.Headers.Referer.ToString(),
                _configuration["Spotify:ClientId"],
                _configuration["Spotify:ClientSecret"],
                _configuration["Spotify:Redirect_URI"]
                );
            SaveToken = newLogin;
            string jsonString = JsonSerializer.Serialize("TokenObtenido");
            return Ok(jsonString);
        }

        public async Task<IActionResult> Index()
        {
            var newReleases = await GetNewReleases();
            string jsonString = JsonSerializer.Serialize(newReleases);
            return Ok(jsonString);
        }

        public async Task<IActionResult> Index2()
        {
            var newUserPlaylist = await GetUserPlaylist();
            string jsonString = JsonSerializer.Serialize(newUserPlaylist);
            return Ok(jsonString);
        }

        public async Task<IActionResult> Index3()
        {
            var newRecentlyPlayedTracks = await GetRecentlyPlayedTracks();
            string jsonString = JsonSerializer.Serialize(newRecentlyPlayedTracks);
            return Ok(jsonString);
        }
        public async Task<IActionResult> Index4()
        {
            var newCurrentlyPlayingTrack = await GetCurrentlyPlayingTrack();
            string jsonString = JsonSerializer.Serialize(newCurrentlyPlayingTrack);
            Console.WriteLine("repitomusicaactual");
            return Ok(jsonString);
        }

       

        public async Task<IEnumerable<Release>> GetNewReleases()
        {
            var tokenResult = SaveToken;
            Console.WriteLine(tokenResult);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/browse/new-releases?country=pe");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Authorization", "Bearer " + tokenResult);
            var response = await client.SendAsync(request);
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

        public async Task<string> GetUserID()
        {
            var tokenResult = SaveToken;
            Console.WriteLine("repitoID");
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/me");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Authorization", "Bearer " + tokenResult);
            var response = await client.SendAsync(request);
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetUser>(responseStream);
            var UserId = responseObject.id;
            string jsonString = JsonSerializer.Serialize(UserId);
            return jsonString;
        }

        public async Task<IEnumerable<UserPlaylists>> GetUserPlaylist()
        //image,musica, artista
        {
            var tokenResult = SaveToken;
            Console.WriteLine(tokenResult);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/me/playlists");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Authorization", "Bearer " + tokenResult);
            var response = await client.SendAsync(request);
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetUserPlaylist>(responseStream);
            var item = responseObject.items;
            return responseObject?.items.Select(i => new UserPlaylists
            {
                Name = i.name,
                URL = i.external_urls.spotify,
                ImageUrl = i.images.FirstOrDefault().url,
                Description = i.description
            }
            );
        }



        public async Task<IEnumerable<RecentlyPlayedTracks>> GetRecentlyPlayedTracks()
        //image,musica, artista
        {
            var tokenResult = SaveToken;
            //Console.WriteLine(tokenResult);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/me/player/recently-played");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Authorization", "Bearer " + tokenResult);
            var response = await client.SendAsync(request);
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

        
        public async Task<CurrentlyPlayingTrack> GetCurrentlyPlayingTrack()
        //image,musica, artista
        {
            var tokenResult = SaveToken;
            //Console.WriteLine(tokenResult);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/me/player/currently-playing");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Authorization", "Bearer " + tokenResult);
            var response = await client.SendAsync(request);
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


        /*
        private async Task<IEnumerable<Release>> GetReleases()
        {
            try
            {
                var token = await _spotifyAccountService.GetToken(
                    _configuration["Spotify:ClientId"],
                    _configuration["Spotify:ClientSecret"]);

                var newReleases = await _spotifyService.GetNewReleases("SE", 10, token);
                return newReleases;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return Enumerable.Empty<Release>();
        }
        */


        /*
        public async Task<string> LoginHTTP()
        {
            var clientID = _configuration["Spotify:ClientId"];
            var endpoint = "https://accounts.spotify.com/authorize";
            var redirectUri = _configuration["Spotify:Redirect_URI"];
            string[] scopes = {Scopes.UserReadEmail,
                               Scopes.UserReadPrivate,
                               Scopes.UserReadCurrentlyPlaying,
                               Scopes.UserReadRecentlyPlayed,
                               Scopes.UserReadPlaybackState,
                               Scopes.UserTopRead,
                               Scopes.UserModifyPlaybackState,
                               Scopes.UserLibraryRead,
                               Scopes.PlaylistReadPrivate};

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{endpoint}?client_id={clientID}&response_type=code&redirect_uri={redirectUri}&scope={string.Join("%20", scopes)}&show_dialog=true");
            if(!Page.IsCallback)
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = response.RequestMessage.RequestUri.AbsoluteUri.ToString();
            var loginURL = Convert.ToBase64String(Encoding.UTF8.GetBytes(responseStream));
            Console.WriteLine(loginURL);
            string strJson = JsonSerializer.Serialize(loginURL);
            //Console.WriteLine(strJson);
            return strJson;
        }
        */






    }
}
