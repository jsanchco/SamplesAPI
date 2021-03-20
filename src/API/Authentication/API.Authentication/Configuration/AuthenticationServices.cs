using Microsoft.AspNetCore.Authentication;
using Shared.Authentication.Model;
using Shared.Authentication.Server;
using System;

namespace API.Authentication.Configuration
{
    public static class AuthenticationServices
    {
        public static AuthenticationBuilder AddHMACAuthentication(this AuthenticationBuilder builder)
        {
            return builder.AddHMACAuthentication((options) => { });
        }

        public static AuthenticationBuilder AddHMACAuthentication(this AuthenticationBuilder builder, Action<HMACAuthenticationOptions> options)
        {
            return builder.AddScheme<HMACAuthenticationOptions, HMACAuthenticationHandler>(HMACAuthenticationOptions.DefaultSchema, options);
        }
    }
}
