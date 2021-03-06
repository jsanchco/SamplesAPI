using System;
using System.Security.Cryptography;
using System.Text;

namespace Shared.Authentication.Helpers
{
    public static class SignatureHelper
    {
        public static string Generate(
            DateTimeOffset requestDate, 
            long contentLenght, 
            string method, 
            string path, 
            string query)
        {
            if (requestDate == default(DateTimeOffset))
                throw new ArgumentException("Request date should be diffrent the default", nameof(requestDate));

            return (requestDate.ToString("r") + '\n' +
                   contentLenght + '\n' +
                   method + '\n' +
                   path + '\n' +
                   query?.TrimStart('?')).ToLower();
        }

        public static string Calculate(byte[] secret, string signature)
        {
            if (secret == null)
                throw new ArgumentNullException(nameof(secret));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            using (var hmac = new HMACSHA256())
            {
                hmac.Key = secret;

                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(signature)));
            }
        }

        public static string Calculate(string secret, string signature)
        {
            if (secret == null)
                throw new ArgumentNullException(nameof(secret));

            if (signature == null)
                throw new ArgumentNullException(nameof(signature));

            using (var hmac = new HMACSHA256())
            {
                hmac.Key = Convert.FromBase64String(secret);

                return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(signature)));
            }
        }
    }
}
