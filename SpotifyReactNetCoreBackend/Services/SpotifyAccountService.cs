using SpotifyReactNetCoreBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpotifyReactNetCoreBackend.Services
{
    public class SpotifyAccountService : ISpotifyAccountService
    {
        private readonly HttpClient _httpClient;

        public SpotifyAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /*
        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "token");

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string> 
            {
                {"grant_type", "client_credentials"}
            });

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

            return authResult.access_token;
        }
        */
        
        public async Task<string> GetToken2(string URL, string client_ID , string clientSecret,string redirect_URI)
        {  //string URL = HttpContext.Request.Headers.Referer.ToString();
            string URLreduce = URL[32..]; //Substring
            string[] parameters = URLreduce.Split('&');
            string[] extracCode = parameters[0].Split('=');
            string[] extractStatus = parameters[1].Split('=');
            string code = extracCode[1];
            string state = extractStatus[1];
            PostGetToken TokenRequest = new();
            {
                TokenRequest.Grant_type = "authorization_code";
                TokenRequest.Code = code;
                TokenRequest.Redirect_uri = redirect_URI;
                TokenRequest.Authorization = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(client_ID + ":" + clientSecret));
            }

            var request = new HttpRequestMessage(HttpMethod.Post, "api/token");
            request.Headers.Add("Authorization", TokenRequest.Authorization);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", TokenRequest.Code},
                { "grant_type",TokenRequest.Grant_type},
                { "redirect_uri", TokenRequest.Redirect_uri }
            });
            var response = await _httpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();
            
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var tokenResult = await JsonSerializer.DeserializeAsync<TokenResult>(responseStream);
            /*
            if (SaveToken == null)
            {
                SaveToken = postResult.access_token;
            }
            */
            return tokenResult.access_token;
        }
    }
}
