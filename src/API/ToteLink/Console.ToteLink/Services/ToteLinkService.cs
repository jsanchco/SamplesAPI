using ConsoleApp.ToteLink.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ToteLink.Services
{
    public class ToteLinkService
    {
        private readonly ILogger<ToteLinkService> _logger;
        private readonly HttpClient _client;

        private const string _accountId = "520000126";
        private const string _urlLocal = "https://localhost:44335/api/v1/ingresarcredito";
        private const string _urlAzure = "https://unitedtotelinkpaymentsproxyapi-pa.azurewebsites.net/api/v1/ingresarcredito";

        public ToteLinkService(
            HttpClient client,
            ILogger<ToteLinkService> logger)
        {
            _logger = logger;
            _client = client;
        }

        public async Task PuntoPagoIngresarCredito_Local()
        {
            try
            {
                var requestPuntoPagoIngresarCredito = new RequestPuntoPagoIngresarCredito
                {
                    AccountId = _accountId,
                    Amount = new decimal(100.45),
                    Currency = "USD",
                    ReferenceId = "435884682"
                };
                var serializedRequestPuntoPagoIngresarCredito = JsonConvert.SerializeObject(requestPuntoPagoIngresarCredito);
                var content = new StringContent(serializedRequestPuntoPagoIngresarCredito, Encoding.UTF8, "application/json");

                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Add("X-API-Key", "5b19c840-4c07-49be-9b71-6945bb176761");

                var response = await _client.PostAsync(_urlLocal, content);
                response.EnsureSuccessStatusCode();

                var contentString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultRequest<ResultPuntoPagoIngresarCredito>>(contentString);
                _logger.LogInformation($"Result: {JsonConvert.SerializeObject(result)}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PuntoPagoIngresarCredito_Local: {ex.Message}");
            }
        }

        public async Task PuntoPagoIngresarCredito_Azure()
        {
            try
            {
                var requestPuntoPagoIngresarCredito = new RequestPuntoPagoIngresarCredito
                {
                    AccountId = _accountId,
                    Amount = new decimal(100.45),
                    Currency = "USD",
                    ReferenceId = "435884685"
                };
                var serializedRequestPuntoPagoIngresarCredito = JsonConvert.SerializeObject(requestPuntoPagoIngresarCredito);
                var content = new StringContent(serializedRequestPuntoPagoIngresarCredito, Encoding.UTF8, "application/json");

                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Add("X-API-Key", "5b19c840-4c07-49be-9b71-6945bb176761");

                var response = await _client.PostAsync(_urlAzure, content);
                response.EnsureSuccessStatusCode();

                var contentString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultRequest<ResultPuntoPagoIngresarCredito>>(contentString);
                _logger.LogInformation($"Result: {JsonConvert.SerializeObject(result)}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in PuntoPagoIngresarCredito_Azure: {ex.Message}");
            }
        }
    }
}
