using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyReactNetCoreBackend.Models;

namespace SpotifyReactNetCoreBackend.Services
{
    public interface ISpotifyAccountService
    {
        Task<string> GetToken2(string url, string client_ID, string clientSecret, string redirect_URI);
    }
}
