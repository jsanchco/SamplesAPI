using ConsoleApp.Authentication.Models;
using Shared.Authentication.Helpers;
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
        private readonly Configuration<AppConfig> _configuration;
        private readonly HttpClient _client;

        public HMACService(
            HttpClient client,
            Configuration<AppConfig> configuration)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task<ResponseResult<string>> GetEcho(string echo, string apiId)
        {
            var endpoint = _configuration.Settings.Endpoints.FirstOrDefault(x => x.ApiId == apiId);
            if (endpoint == null)
            {
                Console.WriteLine($"Failed to load Endpoint {apiId}");
                throw new Exception($"Failed to load Endpoint {apiId}");
            }

            // Create request
            var requestUri = $"{endpoint.Url}/{echo}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Date = DateTimeOffset.UtcNow;

            // Calculate Signature
            string authenticationSignature = SignatureHelper.Calculate(
                endpoint.ApiKey,
                SignatureHelper.Generate(
                    requestMessage.Headers.Date.Value,
                    requestMessage?.Content?.Headers.ContentLength ?? 0,
                    requestMessage.Method.Method,
                    requestMessage.RequestUri.PathAndQuery,
                    ""));
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                endpoint.Scheme,
                endpoint.ApiId + ":" + authenticationSignature);

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
