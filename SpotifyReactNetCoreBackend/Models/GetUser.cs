namespace SpotifyReactNetCoreBackend.Models
{

    public class GetUser
    {
        public string display_name { get; set; }
        public External_Urls external_urls { get; set; }
        public Followers followers { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public object[] images { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }


}
