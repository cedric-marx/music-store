using MusicStore.Microservices.Products.Api;
using Serilog;

Host.CreateDefaultBuilder(args)
    .ConfigureLogging(loggingConfiguration =>
        loggingConfiguration.ClearProviders())
    .UseSerilog((hostingContext, loggerConfiguration) =>
        loggerConfiguration.ReadFrom
            .Configuration(hostingContext.Configuration)).ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    }).Build().Run();