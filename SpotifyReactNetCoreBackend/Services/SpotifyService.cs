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
        /*
        public async Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"browse/new-releases?country={countryCode}&limit={limit}");

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();


            var responseObject = await JsonSerializer.DeserializeAsync<GetNewReleaseResult>(responseStream);
            var items = responseObject.albums.items;
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
        */

        public async Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken)
        {
            Console.WriteLine(accessToken);
            var client = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://api.spotify.com/v1/browse/new-releases?country=pe");
            request.Method = HttpMethod.Get;

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
    }
}
