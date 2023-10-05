using AlerterScraper.Drinnies;
using Custom.Alerter.Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using AlerterScraper;
using AlerterEmailGeneration;

[assembly: FunctionsStartup(typeof(AlerterFunctions.Startup))]
namespace AlerterFunctions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        // builder.Services.AddHttpClient();
        builder.Services.AddScraperServices();
        builder.Services.AddScoped<IDrinniesEmailGenerator, DrinniesEmailGenerator>();
    }
}