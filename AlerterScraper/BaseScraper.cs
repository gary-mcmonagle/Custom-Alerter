namespace AlerterScraper;

public abstract class BaseScraper
{
    protected readonly HttpClient _httpClient;
    public BaseScraper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
