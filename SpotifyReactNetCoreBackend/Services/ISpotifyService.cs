using SpotifyReactNetCoreBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyReactNetCoreBackend.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken);
        Task<string> GetUserID(string accessToken);
        Task<IEnumerable<UserPlaylists>> GetUserPlaylist(string accessToken);
        Task<IEnumerable<RecentlyPlayedTracks>> GetRecentlyPlayedTracks(string accessToken);
        Task<CurrentlyPlayingTrack> GetCurrentlyPlayingTrack(string accessToken);
        Task<IEnumerable<FollowedArtists>> GetFollowedArtists(string accessToken, int limit);
        Task<IEnumerable<PlaylistItems>> GetPlaylistTracks(string accessToken, string idplaylist);
    }
}
