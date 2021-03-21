using ConsoleApp.Authentication.Models;
using Shared.Authentication.Helpers;
using Shared.Authentication.Services;
using Shared.Model.Response;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp.Authentication.Services
{
    public class HMACService
    {
        private const string AUTHENTICATION_SCHEME = "HMAC";

        private readonly Configuration<AppConfig> _configuration;
        private readonly HttpClient _client;
        private readonly ISecretLookup _secretLookup;

        private readonly Endpoint _endpointHMAC;

        public HMACService(
            HttpClient client,
            Configuration<AppConfig> configuration,
            ISecretLookup secretLookup)
        {
            _configuration = configuration;
            var endpointHMAC = _configuration.Settings.Endpoints.FirstOrDefault(x => x.Type == Constants.HMAC);
            if (endpointHMAC == null)
            {
                Console.WriteLine($"Failed to load Endpoint HMAC");
                throw new Exception($"Failed to load Endpoint HMAC");
            }

            _endpointHMAC = endpointHMAC;
            _client = client;            
            _secretLookup = secretLookup;
            _secretLookup.Id = endpointHMAC.Id;
        }

        public async Task<ResponseResult<string>> GetEcho(string echo)
        {
            // Create request
            var requestUri = $"{_endpointHMAC.Url}/{echo}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Date = DateTimeOffset.UtcNow;

            // Calculate Signature
            string authenticationSignature = SignatureHelper.Calculate(
                _secretLookup.Secret,
                SignatureHelper.Generate(
                    requestMessage.Headers.Date.Value,
                    requestMessage?.Content?.Headers.ContentLength ?? 0,
                    requestMessage.Method.Method,
                    requestMessage.RequestUri.PathAndQuery,
                    ""));
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                AUTHENTICATION_SCHEME,
                _secretLookup.Id + ":" + authenticationSignature);

            // Send request
            var httpResponseMessage = await _client.SendAsync(requestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseString = await httpResponseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                Console.WriteLine("GET - HTTP Status: {0}, Reason {1}", httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase);

                return JsonSerializer.Deserialize<ResponseResult<string>>(responseString);
            }
            else
            {
                Console.WriteLine("Failed to call the API. HTTP Status: {0}, Reason {1}", httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase);
                return null;
            }
        }
    }
}
