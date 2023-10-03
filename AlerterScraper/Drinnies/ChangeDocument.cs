namespace AlerterScraper.Drinnies;

public record ChangeDocument
{
    public IEnumerable<Variant> NewProducts { get; set; } = new List<Variant>();
    public IEnumerable<Variant> BackInStock { get; set; } = new List<Variant>();
}
