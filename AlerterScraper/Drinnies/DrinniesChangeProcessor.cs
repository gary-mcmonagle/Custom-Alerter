
namespace AlerterScraper.Drinnies;

public class DrinniesChangeProcessor : IDrinniesChangeProcessor
{
    public ChangeDocument GetChanges(IEnumerable<Product> master, IEnumerable<Product> updated)
    {
        var newVariants = new List<Variant>();

        Console.WriteLine($"Master count: {master.Count()}");
        Console.WriteLine($"Updated count: {updated.Count()}");

        var updatedVariants = updated.SelectMany(x => x.Variants);
        var masterVariants = master.SelectMany(x => x.Variants);
        foreach (var variant in updatedVariants)
        {
            var masterVariant = masterVariants.FirstOrDefault(x => x.Id == variant.Id);
            if (masterVariant == null)
            {
                newVariants.Add(variant);
            }
            else
            {
                // Check for stock changes
            }
        }
        return new ChangeDocument { NewProducts = newVariants };
    }
}
