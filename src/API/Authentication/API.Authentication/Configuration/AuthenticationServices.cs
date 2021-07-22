using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Authentication.Model;
using Shared.Authentication.Server;
using Shared.Authentication.Services.CacheClientsAuthenticate;
using System;

namespace API.Authentication.Configuration
{
    public static class AuthenticationServices
    {
        public static AuthenticationBuilder AddHMACAuthentication(
            this AuthenticationBuilder builder)
        {
            builder.AddHMACAuthentication((options) => { });
            builder.AddHMAC1Authentication((options) => { });

            return builder;
        }

        public static AuthenticationBuilder AddHMACAuthentication(this AuthenticationBuilder builder, Action<AuthenticationOptionsBase> options)
        {
            return builder.AddScheme<AuthenticationOptionsBase, HMACAuthenticationHandler>("HMAC", options);
        }

        public static AuthenticationBuilder AddHMAC1Authentication(this AuthenticationBuilder builder)
        {
            builder.AddHMAC1Authentication((options) => { });

            return builder;
        }

        public static AuthenticationBuilder AddHMAC1Authentication(this AuthenticationBuilder builder, Action<AuthenticationOptionsBase> options)
        {
            return builder.AddScheme<AuthenticationOptionsBase, HMACAuthenticationHandler>("HMAC1", options);
        }

        public static IServiceCollection AddCacheClientsAuthenticate(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("AuthenticationSettings").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);

            services.AddSingleton<ICacheClientsAuthenticateService, CacheClientsAuthenticateService>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "HMAC";
            }).AddHMACAuthentication();

            return services;
        }
    }
}
