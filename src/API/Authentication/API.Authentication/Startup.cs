using API.Authentication.Configuration;
using API.Authentication.Models;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Authentication.Services;
using Shared.Logger;

namespace API.Authentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCustomHealthChecks();

            var hMACAuthenticationSettings = new HMACAuthenticationSettings();
            Configuration.GetSection("HMACAuthenticationSettings").Bind(hMACAuthenticationSettings);
            services.AddScoped<ISecretLookup>(x => new SecretLookup(
                hMACAuthenticationSettings.Secret,
                new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 }));

            services.AddMemoryCache();
            //services.AddAuthentication(o => o.AddScheme("api", a => a.HandlerType = typeof(HMACAuthentication.API.TokenHandler)));
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "HMAC";
            }).AddHMACAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticationRequired", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Sample Authentication API",
                    Description = "Authentication API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Check url -> https://my.papertrailapp.com/events
            loggerFactory.AddSyslog(
                Configuration.GetValue<string>("Papertrail:host"),
                Configuration.GetValue<int>("Papertrail:port"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "v1 docs");
                //s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}
