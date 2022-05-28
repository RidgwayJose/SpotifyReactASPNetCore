namespace SpotifyReactNetCoreBackend.Models
{
    public class PostGetToken
    {
        public string Grant_type { get; set; }
        public string Code { get; set; }
        public string Redirect_uri { get; set; }
        public string Authorization { get; set; }
    }
}
