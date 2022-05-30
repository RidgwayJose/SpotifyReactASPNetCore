using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyReactNetCoreBackend.Models
{
    public class FollowedArtists
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Followers { get; set; }
        public int Popularity { get; set; }
    }
}
