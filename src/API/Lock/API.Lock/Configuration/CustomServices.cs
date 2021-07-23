using API.Lock.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Lock.Configuration
{
    public static class CustomServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IDoSomethingService, DoSomethingService>();
            //services.AddScoped<IDoSomethingService, DoSomethingService>();

            return services;
        }
    }
}
