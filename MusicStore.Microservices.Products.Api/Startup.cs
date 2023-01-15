using System.Reflection;
using MusicStore.Microservices.Products.Data;

namespace MusicStore.Microservices.Products.Api;

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
        serviceCollection.ConfigureCors(Configuration);
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.ConfigureDatabaseConfiguration(Configuration);
        serviceCollection.ConfigureServices();
        serviceCollection.ConfigureRepositories();
        serviceCollection.ConfigureMassTransit(Configuration);
        serviceCollection.ConfigureControllers();
        serviceCollection.ConfigureSwagger();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.MigrateAndSeedDatabase<ProductsDbContext>(_ => { });

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Product API"));
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseCors();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}