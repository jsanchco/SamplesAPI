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
        public static AuthenticationBuilder AddHMACAuthentication(this AuthenticationBuilder builder)
        {
            return builder.AddHMACAuthentication((options) => { });
        }

        public static AuthenticationBuilder AddHMACAuthentication(this AuthenticationBuilder builder, Action<HMACAuthenticationOptions> options)
        {
            return builder.AddScheme<HMACAuthenticationOptions, HMACAuthenticationHandler>(HMACAuthenticationOptions.DefaultSchema, options);
        }

        public static IServiceCollection AddCacheClientsAuthenticate(this IServiceCollection services, IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();
            configuration.GetSection("AuthenticationSettings").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);

            services.AddSingleton<ICacheClientsAuthenticateService, CacheClientsAuthenticateService>();

            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = "HMAC";
            }).AddHMACAuthentication();

            return services;
        }
    }
}
