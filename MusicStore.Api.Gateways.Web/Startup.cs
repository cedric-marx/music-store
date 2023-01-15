using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Kubernetes;

namespace MusicStore.Api.Gateways.Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddHealthChecks();
        serviceCollection.AddControllers();
        serviceCollection.AddOcelot().AddKubernetes();
        serviceCollection.ConfigureCors(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseCors();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
        
        app.UseOcelot().Wait();
    }
}