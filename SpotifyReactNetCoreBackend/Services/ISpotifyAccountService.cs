using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyReactNetCoreBackend.Models;

namespace SpotifyReactNetCoreBackend.Services
{
    public interface ISpotifyAccountService
    {
        Task<string> GetToken(PostGetToken Code);
    }
}
