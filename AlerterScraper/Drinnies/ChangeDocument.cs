namespace AlerterScraper.Drinnies;

public record ChangeDocument
{
    public IEnumerable<ChangedProduct> NewProducts { get; set; } = new List<ChangedProduct>();
    public IEnumerable<ChangedProduct> BackInStock { get; set; } = new List<ChangedProduct>();
}

public record ChangedProduct
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string VariantDescription { get; init; } = string.Empty;
}
