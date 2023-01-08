using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
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
    
    public static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "API Gateway Web",
            Description = "The Ocelot API Gateway for web applications",
            TermsOfService = new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "Example Contact",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example License",
                Url = new Uri("https://example.com/license")
            }
        }));
    }
}