using System.Linq;
using System.Threading.Tasks;

namespace Shared.Authentication.Services.CacheClientsAuthenticate
{
    public class CacheClientsAuthenticateService : ICacheClientsAuthenticateService
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public CacheClientsAuthenticateService(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> FindAsync(string id)
        {
            var findAuthentication = _authenticationSettings.Authentications.FirstOrDefault(x => x.ApiId == id);

            if (findAuthentication == null)
                return await Task.FromResult<string>(null);

            return await Task.FromResult(findAuthentication.ApiKey);
        }
    }
}
