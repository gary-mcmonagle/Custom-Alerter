
using System.Net.Http.Json;

namespace AlerterScraper.Drinnies;

public class DrinniesProductScraper : BaseScraper, IDrinniesProductScraper
{
    private const string _productUrl = "https://api.loveyourartist.com/v1/store/products?filter[profiles][$eq]=63691ad05d62f1822d66e670";
    public DrinniesProductScraper(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        var resp = await _httpClient.GetAsync(_productUrl);
        return await resp.Content.ReadFromJsonAsync<IEnumerable<Product>>() ?? new List<Product>();
    }
}
