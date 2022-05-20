namespace SpotifyReactNetCoreBackend.Models
{
    public class DataLogin
    {
        public string endpoint { get; set; }
        public string clientId { get; set; }
        public string redirectUri { get; set; }
        public string[] scopes { get; set; }
    }
}
