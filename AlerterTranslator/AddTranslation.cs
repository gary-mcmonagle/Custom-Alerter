using Microsoft.Extensions.DependencyInjection;

namespace AlerterTranslator;

public static class AddTranslation
{
    public static IServiceCollection AddTranslationServices(this IServiceCollection services, string apiKey, string region)
    {
        services.AddHttpClient<ITranslationService, TranslationService>();
        services.AddScoped<ITranslationService>(x =>
        {
            var httpClient = x.GetRequiredService<HttpClient>();
            return new TranslationService(httpClient, apiKey, region);
        });
        return services;
    }
}
