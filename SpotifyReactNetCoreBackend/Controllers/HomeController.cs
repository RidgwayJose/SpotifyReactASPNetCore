using Microsoft.AspNetCore.Mvc;
using SpotifyReactNetCoreBackend.Services;
using SpotifyReactNetCoreBackend.Models;
using System.Text.Json;

namespace SpotifyReactNetCoreBackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly ISpotifyAccountService _spotifyAccountService;
        private readonly IConfiguration _configuration;
        ISpotifyService _spotifyService;

        public HomeController(
            ISpotifyAccountService spotifyAccountService,
            IConfiguration configuration,
            ISpotifyService spotifyService)
        {
            _spotifyAccountService = spotifyAccountService;
            _configuration = configuration;
            _spotifyService = spotifyService;
        }
        public async Task<IActionResult> Index()
        {
            var newReleases = await GetReleases();

            string jsonString = JsonSerializer.Serialize(newReleases);
            Console.WriteLine(jsonString);
            return Ok(jsonString);
        }

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

        public string Login()
        {


            var endpoint = "https://accounts.spotify.com/authorize";
            var clientID = _configuration["Spotify:ClientId"];
            var redirectUri = "https://localhost:3000/";
            string[] scopes = {Scopes.UserReadCurrentlyPlaying,
                Scopes.UserReadRecentlyPlayed,
                Scopes.UserReadPlaybackState,
                Scopes.UserTopRead,
                Scopes.UserModifyPlaybackState };

            /*
            var endpoint = "https://accounts.spotify.com/authorize";
            var clientID = "1bdda459e75f4754863c0527eaa41b5b";
            var redirectUri = "http://localhost:3001/";
            string[] scopes = { "user-read-currently-playing", "user-read-recently-played", "user-read-playback-state", "user-top-read", "user-modify-playback-state"};
            string prueba = string.Join("20%",scopes);
            */
           
            var loginURL = $"{endpoint}?client_id={clientID}&response_type=token&redirect_uri={redirectUri}&scope={string.Join("%20", scopes)}&show_dialogue=true";
            
        
            Console.WriteLine(loginURL);
            string strJson = JsonSerializer.Serialize(loginURL);
            Console.WriteLine(strJson);
            return strJson;
        }
    }
}
