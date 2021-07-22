using API.Authentication;
using API.Authentication.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Shared.Authentication.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test.Authentication.IntegrationTest
{
    [TestClass]
    public class EchoContollerTests
    {
        private static WebApplicationFactory<Startup> _factory;
        private TestController TestController;
        private const string _accountId = "4206 8556 34 7456749714";

        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Startup>();

            var logger = Mock.Of<ILogger<TestController>>();

            TestController = new TestController(logger);
        }

        [TestMethod]
        public async Task GetEcho_ReturnOk()
        {
            var client = _factory.CreateClient();
            var url = @"http://localhost:60378/api/v1/Test/echo/hola";

            var requestEchoSerialized = JsonConvert.SerializeObject("Hola!!");

            var requestMessage = CreateHMACRequest(url, HttpMethod.Get, null);
            var responseMessage = await client.SendAsync(requestMessage);

            Assert.AreEqual(responseMessage.IsSuccessStatusCode, true);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }

        #region Auxiliary Functions

        private HttpRequestMessage CreateHMACRequest(
            string uri,
            HttpMethod method,
            object data)
        {
            var scheme = "HMAC";
            var key = "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=";
            var apiId = "Device1";

            var query = JsonConvert.SerializeObject(data);
            var requestMessage = new HttpRequestMessage(method, uri);
            requestMessage.Headers.Date = DateTimeOffset.UtcNow;

            // Calculate Signature
            string authenticationSignature = SignatureHelper.Calculate(
                key,
                SignatureHelper.Generate(
                    requestMessage.Headers.Date.Value,
                    requestMessage?.Content?.Headers.ContentLength ?? 0,
                    requestMessage.Method.Method,
                    requestMessage.RequestUri.PathAndQuery,
                    query));
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                scheme,
                apiId + ":" + authenticationSignature);

            return requestMessage;
        }

        #endregion
    }
}
