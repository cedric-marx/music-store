using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicStore.Api.Gateways.Web.Configurations;

namespace MusicStore.Api.Gateways.Web;

internal static class StartupExtensions
{
    public static void ConfigureCors(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var corsConfiguration = configuration.GetSection(nameof(CorsConfiguration)).Get<CorsConfiguration>();
        serviceCollection.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(corsConfiguration?.Origins.Split(",") ?? Array.Empty<string>());
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.SetIsOriginAllowedToAllowWildcardSubdomains();
            });
        });
    }
}