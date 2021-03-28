using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Authentication.Model;
using Shared.Authentication.Services.CacheClientsAuthenticate;
using System;
using System.Linq;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Shared.Authentication.Server
{
    public class HMACAuthenticationHandler : AuthenticationHandler<AuthenticationOptionsBase>
    {
        private const string DateHeader = "Date";
        private const string AuthorizationHeader = "Authorization";

        private readonly ICacheClientsAuthenticateService _cacheClientsAuthenticateService;

      //  public HMACAuthenticationHandler(
      //IOptionsMonitor<HMACAuthenticationOptions> options,
      //ILoggerFactory logger,
      //UrlEncoder encoder,
      //ISystemClock clock,
      //ICacheClientsAuthenticateService cacheClientsAuthenticateService) : base(options, logger, encoder, clock)
      //  {
      //      _cacheClientsAuthenticateService = cacheClientsAuthenticateService ??
      //          throw new ArgumentNullException(nameof(cacheClientsAuthenticateService));
      //  }

        public HMACAuthenticationHandler(
              IOptionsMonitor<AuthenticationOptionsBase> options,
              ILoggerFactory logger,
              UrlEncoder encoder,
              ISystemClock clock,
              ICacheClientsAuthenticateService cacheClientsAuthenticateService) : base(
                  options, 
                  logger, 
                  encoder, 
                  clock)
        {
            _cacheClientsAuthenticateService = cacheClientsAuthenticateService ??
                throw new ArgumentNullException(nameof(cacheClientsAuthenticateService));           
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var header = SplitAuthenticationHeader();
            if (header == null)
                return AuthenticateResult.NoResult();

            // Verify that request data is within acceptable time
            if (!DateTimeOffset.TryParseExact(Request.Headers[DateHeader], "r", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeUniversal, out DateTimeOffset requestDate))
                return AuthenticateResult.Fail("Unable to parse Date header");

            if (requestDate > Clock.UtcNow.Add(Options.AllowedDateDrift) ||
                requestDate < Clock.UtcNow.Subtract(Options.AllowedDateDrift))
                return AuthenticateResult.Fail("Date is drifted more than allowed, adjust your time settings.");

            // Lookup and verify secret
            Logger.LogDebug("Looking up secret for {Id}", header.Value.id);
            var secret = await _cacheClientsAuthenticateService.FindAsync(
                Scheme.Name,
                header.Value.id);

            if (secret == null)
            {
                Logger.LogInformation("No secret found for {Id}", header.Value.id);
                return AuthenticateResult.Fail("Invalid id");
            }

            // Check signature
            string serverSignature = Helpers.SignatureHelper.Calculate(
                secret,
                Helpers.SignatureHelper.Generate(
                    requestDate,
                    Request.ContentLength ?? 0,
                    Request.Method,
                    Request.Path,
                    Request.QueryString.Value));
            Logger.LogDebug("Calculated server side signature {signature}", serverSignature);

            if (serverSignature.Equals(header.Value.signature))
            {
                return AuthenticateResult.Success(new AuthenticationTicket(
                    new GenericPrincipal(new GenericIdentity(header.Value.id), Options.GetRolesForId?.Invoke(header.Value.id) ?? null),
                    new AuthenticationProperties() { IsPersistent = false, AllowRefresh = false },
                    Options.Schema));
            }
            else
                return AuthenticateResult.Fail("Invalid signature");
        }

        #region Auxiliary Functions

        private (string id, string signature)? SplitAuthenticationHeader()
        {
            var headerContent = Request.Headers[AuthorizationHeader].SingleOrDefault();
            if (headerContent == null)
                return null;

            var splitHeader = headerContent.Split(' ', ':');
            if (splitHeader.Length != 3)
                return null;

            return (splitHeader[1], splitHeader[2]);
        }

        #endregion
    }
}
