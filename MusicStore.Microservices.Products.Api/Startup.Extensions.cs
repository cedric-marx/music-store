using System.Data.Common;
using System.Globalization;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MusicStore.Microservices.Orders.Events.Events;
using MusicStore.Microservices.Products.Api.Configurations;
using MusicStore.Microservices.Products.Api.RequestModels;
using MusicStore.Microservices.Products.Business.Services;
using MusicStore.Microservices.Products.Business.Services.Implementation;
using MusicStore.Microservices.Products.Data;
using MusicStore.Microservices.Products.Data.Models;
using MusicStore.Microservices.Products.Data.Repositories;
using MusicStore.Microservices.Products.Events.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Polly;
using Polly.Retry;

namespace MusicStore.Microservices.Products.Api;

internal static class StartupExtensions
{
    private static bool ExceptionsHandledByOrchestrator =>
        false; //Startup.Configuration.GetValue<string>("OrchestratorType")?.ToUpper() == "KUBERNETES";

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

    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductService, ProductService>();
        serviceCollection.AddValidatorsFromAssemblyContaining<ProductRequestModelValidator>();
    }

    public static void ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRepository<Product>, Repository<Product, ProductDbContext>>();
    }

    public static void ConfigureMassTransit(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var rabbitMqConfiguration =
            configuration.GetSection(nameof(RabbitMqConfiguration)).Get<RabbitMqConfiguration>();
        serviceCollection.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host($"rabbitmq://{rabbitMqConfiguration?.Hostname}", host =>
                {
                    host.Username(rabbitMqConfiguration?.Username);
                    host.Password(rabbitMqConfiguration?.Password);
                });
                cfg.ReceiveEndpoint("musicstore-products-microservice", e => { e.ConfigureConsumers(context); });
            });
            
            x.AddConsumersFromNamespaceContaining(typeof(OrderCreatedEventHandler));
        });
    }

    public static void ConfigureDatabaseConfiguration(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var databaseConfiguration =
            configuration.GetSection(nameof(DatabaseConfiguration)).Get<DatabaseConfiguration>();
        serviceCollection.AddDbContext<ProductDbContext>(
            options => options.UseNpgsql(databaseConfiguration?.ConnectionString), ServiceLifetime.Transient);
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddMvc().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.Culture = CultureInfo.InvariantCulture;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddControllersWithViews(options =>
        {
            foreach (var formatter in options.InputFormatters)
                if (formatter is NewtonsoftJsonInputFormatter jsonFormatter)
                    jsonFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
        });
    }

    private static RetryPolicy CreateRetryPolicy<TContext>(int retries, double initialRetryDelay,
        ILogger<TContext> logger)
        where TContext : DbContext
    {
        return Policy.Handle<DbException>()
            .WaitAndRetry(
                retries,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(initialRetryDelay, retryAttempt)),
                (exception, timeSpan, retry, _) =>
                {
                    logger.LogWarning(
                        exception,
                        $"{exception.GetType().Name} thrown while trying to migrate database, but will try again in {timeSpan.TotalSeconds}s... ({retry}/{retries}");
                });
    }

    public static void MigrateDatabase<TContext>(this IApplicationBuilder app)
        where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dataContext = scope.ServiceProvider.GetRequiredService<TContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

        void migrateDatabaseDelegate()
        {
            logger.LogInformation("Trying to migrate database...");

            dataContext.Database.Migrate();

            logger.LogInformation("Database migration was successful.");
        }

        if (ExceptionsHandledByOrchestrator)
        {
            // Orchestrator will handle exceptions.
            migrateDatabaseDelegate();
        }
        else
        {
            var retryPolicy = CreateRetryPolicy(10, 2, logger);
            retryPolicy.Execute(migrateDatabaseDelegate);
        }
    }


    /// <summary>
    ///     Will execute the seed delegate in order to seed the database.
    /// </summary>
    /// <typeparam name="TContext">The type of database context that should be used to seed.</typeparam>
    /// <param name="seedDelegate">The seed delegate that should contain all seeding functionality.</param>
    public static IApplicationBuilder SeedDatabase<TContext>(this IApplicationBuilder app,
        Action<TContext> seedDelegate)
        where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dataContext = scope.ServiceProvider.GetRequiredService<TContext>();
        seedDelegate.Invoke(dataContext);
        return app;
    }

    public static void MigrateAndSeedDatabase<TContext>(this IApplicationBuilder app, Action<TContext> seedDelegate)
        where TContext : DbContext
    {
        MigrateDatabase<TContext>(app);
        SeedDatabase(app, seedDelegate);
    }
    
    public static void ConfigureSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Product API",
            Description = "The API for the product microservice",
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