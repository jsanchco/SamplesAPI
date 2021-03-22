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
        public string Type { get; set; }
        public string Url { get; set; }
        public string ApiId { get; set; }
        public string ApiKey { get; set; }
        public string Id { get; set; }
        public byte[] Secret = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };
    }
}
