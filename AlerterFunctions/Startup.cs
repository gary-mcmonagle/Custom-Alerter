using AlerterScraper.Drinnies;
using Custom.Alerter.Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AlerterScraper;
using AlerterEmailGeneration;
using AlerterTranslator;
using System;

[assembly: FunctionsStartup(typeof(AlerterFunctions.Startup))]
namespace AlerterFunctions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // builder.Services.AddHttpClient();
        builder.Services.AddScraperServices();
        builder.Services.AddTranslationServices(
            Environment.GetEnvironmentVariable("TRANSLATION_API_KEY"),
            Environment.GetEnvironmentVariable("TRANSLATION_REGION")
        );
        builder.Services.AddScoped<IDrinniesEmailGenerator, DrinniesEmailGenerator>();
    }
}