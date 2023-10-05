using AlerterScraper.Drinnies;

namespace AlerterEmailGeneration;

public interface IDrinniesEmailGenerator
{
    public string? GenerateEmail(ChangeDocument changes);
}
