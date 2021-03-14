using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Basic.Configuration
{
    // Health check            
    // - Add NuGet package: AspNetCore.HealthChecks.UI (Version 3.0.9)
    // - Add folder 'healthchecks' to project
    // - Check UI -> http://localhost:XXXXXX/healthchecks-ui#/healthchecks

    public static class HealthChecksServices
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

            services.AddHealthChecksUI();

            return services;
        }
    }
}
