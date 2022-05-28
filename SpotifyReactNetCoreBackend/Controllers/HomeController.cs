﻿using SpotifyReactNetCoreBackend.Models;
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
            _spotifyService = spotifyService;;
        }
        static string SaveToken { set; get; }
        
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

        public string Login()
        {
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
            /*
            var endpoint = "https://accounts.spotify.com/authorize";
            var clientID = "1bdda459e75f4754863c0527eaa41b5b";
            var redirectUri = "http://localhost:3001/";
            string[] scopes = { "user-read-currently-playing", "user-read-recently-played", "user-read-playback-state", "user-top-read", "user-modify-playback-state"};
            string prueba = string.Join("20%",scopes);
            */

            var loginURL = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{endpoint}?client_id={clientID}&response_type=code&redirect_uri={redirectUri}&state={state}&scope={string.Join("%20", scopes)}&show_dialog=true"));



            //Console.WriteLine(loginURL);
            string strJson = JsonSerializer.Serialize(loginURL);
            //Console.WriteLine(strJson);
            return strJson;
        }
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

        public PostGetToken GetCode()
        {
            string URL = HttpContext.Request.Headers.Referer.ToString();
            string URLreduce = URL[32..]; //Substring
            string[] parameters = URLreduce.Split('&');
            string[] extracCode = parameters[0].Split('=');
            string[] extractStatus = parameters[1].Split('=');
            string code = extracCode[1];
            string state = extractStatus[1];
            PostGetToken postGetToken = new();
            {
                postGetToken.Grant_type = "authorization_code";
                postGetToken.Code = code;
                postGetToken.Redirect_uri = _configuration["Spotify:Redirect_URI"];
                postGetToken.Authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration["Spotify:ClientId"] + ":" + _configuration["Spotify:ClientSecret"]));
            }
            return postGetToken;
        }

        public async Task<string> GetToken()
        {
            var atrib = GetCode();
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            var formList =  new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("code", atrib.Code),
                new KeyValuePair<string, string>("grant_type",atrib.Grant_type),
                new KeyValuePair<string, string>("redirect_uri", atrib.Redirect_uri)
            };
            request.Content = new FormUrlEncodedContent(formList);
            request.RequestUri = new Uri("https://accounts.spotify.com/api/token");
            request.Method = HttpMethod.Post;
            request.Headers.Add("Authorization", atrib.Authorization);
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            var postResult = JsonSerializer.Deserialize<TokenResult>(result);
            if (SaveToken == null)
            {
                SaveToken = postResult.access_token;
            }
            return postResult.access_token; 
            }
        
        public async Task<IEnumerable<Release>> GetNewReleases()
        {
            if (SaveToken == null)
            {
                await GetToken();
            }
            var tokenResult = SaveToken;
            Console.WriteLine(tokenResult);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/browse/new-releases?country=pe");
            request.Method = HttpMethod.Get;

            request.Headers.Add("Authorization", "Bearer " + tokenResult);
            var response = await client.SendAsync(request);
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject  = JsonSerializer.Deserialize<GetNewReleaseResult>(responseStream);
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
            if (SaveToken == null)
            {
                await GetToken();
            }
            var tokenResult = SaveToken;
            Console.WriteLine(tokenResult);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/me");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Authorization", "Bearer " + tokenResult);
            var response = await client.SendAsync(request);
            var responseStream = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetUser>(responseStream);
            var UserId = responseObject.id;
            return UserId;
        }

        public async Task<IEnumerable<UserPlaylists>> GetUserPlaylist()
        //image,musica, artista
        {
            if (SaveToken == null)
            {
                await GetToken();
            }
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
            if (SaveToken == null)
            {
                await GetToken();
            }
            var tokenResult = SaveToken;
            Console.WriteLine(tokenResult);
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

    }
}
