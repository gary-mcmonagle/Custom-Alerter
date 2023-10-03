using AlerterScraper.Drinnies;
using Microsoft.Extensions.DependencyInjection;

namespace AlerterScraper;

public static class AddScraper
{
    public static IServiceCollection AddScraperServices(this IServiceCollection services)
    {
        services.AddHttpClient<IDrinniesProductScraper, DrinniesProductScraper>();
        services.AddScoped<IDrinniesProductScraper, DrinniesProductScraper>();
        services.AddScoped<IDrinniesChangeProcessor, DrinniesChangeProcessor>();
        return services;
    }
}
