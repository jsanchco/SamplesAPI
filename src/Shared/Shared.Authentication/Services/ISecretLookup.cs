using System.Threading.Tasks;

namespace Shared.Authentication.Services
{
    public interface ISecretLookup
    {
        public string AppId { get; set; }
        public string AppKey { get; set; }
        public string Id { get; set; }
        public byte[] Secret { get; set; }

        Task<byte[]> LookupAsync(string id);
    }
}
