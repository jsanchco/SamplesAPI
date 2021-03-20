using System.Threading.Tasks;

namespace Shared.Authentication.Services
{
    public interface ISecretLookup
    {
        Task<byte[]> LookupAsync(string id);
    }
}
