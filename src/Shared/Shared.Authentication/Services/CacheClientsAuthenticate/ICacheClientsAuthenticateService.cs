using System.Threading.Tasks;

namespace Shared.Authentication.Services.CacheClientsAuthenticate
{
    public interface ICacheClientsAuthenticateService
    {
        Task<string> FindAsync(string id);
    }
}
