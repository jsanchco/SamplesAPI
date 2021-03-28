using System.Collections.Generic;

namespace Shared.Authentication.Services.CacheClientsAuthenticate
{
    public class AuthenticationSettings
    {
        public List<Authentication> Authentications { get; set; }
    }

    public class Authentication
    {
        public string Schemes { get; set; }
        public string ApiId { get; set; }
        public string ApiKey { get; set; }
    }
}
