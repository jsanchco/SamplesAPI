using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.Serilog.Configuration
{
    // Health check            
    // - Add NuGet package: AspNetCore.HealthChecks.UI (Version 3.1.3)
    // - Add NuGet package: AspNetCore.HealthChecks.UI.InMemory.Storage (Version 3.1.2)
    // - Add NuGet package: AspNetCore.HealthChecks.UI.Client (Version 3.1.2)
    // - Check UI -> http://localhost:60388/healthchecks-ui#/healthchecks

    public static class HealthChecksServices
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency
            }).AddInMemoryStorage();

            services.AddHealthChecks().AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }
    }
}
