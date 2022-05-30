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
        static string SaveCode { set; get; }
        static string SaveToken { set; get; }
        public async Task<string> TokenRequest(string URL, string client_ID, string clientSecret, string redirect_URI)
        {
            string URLreduce = URL[32..]; //Substring
            string[] parameters = URLreduce.Split('&');
            string[] extracCode = parameters[0].Split('=');
            string[] extractStatus = parameters[1].Split('=');
            string code = extracCode[1];
            string state = extractStatus[1];

            if (code != SaveCode)
            {
                SaveCode = code;

                var request = new HttpRequestMessage(HttpMethod.Post, "api/token");

                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(client_ID + ":" + clientSecret)));

                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "code", code },
                    { "grant_type","authorization_code" },
                    { "redirect_uri", redirect_URI }
                });
                
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var tokenResult = await JsonSerializer.DeserializeAsync<TokenResult>(responseStream);
                SaveToken = tokenResult.access_token;
                return SaveToken;
            }
            return SaveToken;
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

    }
}
