namespace AlerterScraper.Drinnies;

public interface IDrinniesProductScraper
{
    public Task<IEnumerable<Product>> GetProducts();
}
