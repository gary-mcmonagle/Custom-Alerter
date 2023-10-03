namespace AlerterScraper.Drinnies;

public interface IDrinniesChangeProcessor
{
    public ChangeDocument GetChanges(IEnumerable<Product> master, IEnumerable<Product> updated);
}
