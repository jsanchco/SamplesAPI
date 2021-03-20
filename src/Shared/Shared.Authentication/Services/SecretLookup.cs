using System.Threading.Tasks;

namespace Shared.Authentication.Services
{
    public class SecretLookup : ISecretLookup
    {
        public const string Id = "Device1";
        public static readonly byte[] Secret = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };

        public Task<byte[]> LookupAsync(string id)
        {
            if (id == SecretLookup.Id)
                return Task.FromResult(SecretLookup.Secret);
            else
                return Task.FromResult<byte[]>(null);
        }
    }
}
