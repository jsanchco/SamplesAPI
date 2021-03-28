using System.Collections.Generic;

namespace ConsoleApp.Authentication.Models
{
    public class Constants
    {
        public static string HMAC = "HMAC";
    }

    public class AppConfig
    {
        public string Title { get; set; }
        public Papertrail Papertrail { get; set; } = new Papertrail();
        public IList<Endpoint> Endpoints { get; set; }
    }

    public class Papertrail
    {
        public string host { get; set; }
        public int port { get; set; }
    }

    public class Endpoint
    {
        public string Scheme { get; set; }
        public string Url { get; set; }
        public string ApiId { get; set; }
        public string ApiKey { get; set; }
    }
}
