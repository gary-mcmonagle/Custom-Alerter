namespace AlerterScraper.Drinnies;

public interface IDrinniesChangeProcessor
{
    public Task<ChangeDocument> GetChanges(IEnumerable<Product> master, IEnumerable<Product> updated);
}
