using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyReactNetCoreBackend.Models
{
    public class UserPlaylists
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string PlaylistID { get; set; }
    }
}
