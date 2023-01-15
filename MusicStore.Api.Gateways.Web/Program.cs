using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MusicStore.Api.Gateways.Web;
using Serilog;

Host.CreateDefaultBuilder(args)
    .ConfigureLogging(loggingConfiguration =>
        loggingConfiguration.ClearProviders())
    .UseSerilog((hostingContext, loggerConfiguration) =>
        loggerConfiguration.ReadFrom
            .Configuration(hostingContext.Configuration)).ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
            .AddJsonFile("ocelot.json")
            .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json")
            .AddEnvironmentVariables();
    }).Build().Run();