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
        private readonly ISpotifyService _spotifyService;

        public HomeController(
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }
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
                               Scopes.UserFollowRead,
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

        public async Task<IActionResult> NewReleases()
        {
            var newReleases = await _spotifyService.GetNewReleases("PE", 10, SaveToken);
            string jsonString = JsonSerializer.Serialize(newReleases);
            return Ok(jsonString);
        }
        public async Task<IActionResult> GetUserID()
        {
            var newReleases = await _spotifyService.GetUserID(SaveToken);
            string jsonString = JsonSerializer.Serialize(newReleases);
            return Ok(jsonString);
        }

        public async Task<IActionResult> UserPlaylist()
        {   
            var newUserPlaylist = await _spotifyService.GetUserPlaylist(SaveToken);
            string jsonString = JsonSerializer.Serialize(newUserPlaylist);
            return Ok(jsonString);
        }

        public async Task<IActionResult> RecentlyPlayedTracks()
        {
            var newRecentlyPlayedTracks = await _spotifyService.GetRecentlyPlayedTracks(SaveToken);
            string jsonString = JsonSerializer.Serialize(newRecentlyPlayedTracks);
            return Ok(jsonString);
        }
        public async Task<IActionResult> CurrentlyPlayingTrack()
        {
            try
            {
                var newCurrentlyPlayingTrack = await _spotifyService.GetCurrentlyPlayingTrack(SaveToken);
                string jsonString = JsonSerializer.Serialize(newCurrentlyPlayingTrack);
                Console.WriteLine("repitomusicaactual");
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return SignOut();
        }

        public async Task<IActionResult> FollowedArtists()
        {
            var newFollowedArtist = await _spotifyService.GetFollowedArtists(SaveToken, 6);
            string jsonString = JsonSerializer.Serialize(newFollowedArtist);
            return Ok(jsonString);
        }

        //2GyMzatj1Tpz3h7IxquXoH
        [Route("{id}")]
        public async Task<IActionResult> PlaylistTracks(string id)
        {
            var newPlaylistTracks = await _spotifyService.GetPlaylistTracks(SaveToken, id);
            string jsonString = JsonSerializer.Serialize(newPlaylistTracks);
            return Ok(jsonString);
        }







    }
}
