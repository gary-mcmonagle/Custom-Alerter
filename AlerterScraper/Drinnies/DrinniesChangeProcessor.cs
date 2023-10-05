
namespace AlerterScraper.Drinnies;

public class DrinniesChangeProcessor : IDrinniesChangeProcessor
{
    public ChangeDocument GetChanges(IEnumerable<Product> master, IEnumerable<Product> updated)
    {
        var newVariants = new List<Variant>();
        var backInStock = new List<Variant>();

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
                if (!masterVariant.InStock && variant.InStock)
                {
                    backInStock.Add(variant);
                }
            }
        }
        return new ChangeDocument { NewProducts = newVariants, BackInStock = backInStock };
    }
}
