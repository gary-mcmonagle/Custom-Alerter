
using System.Security.Cryptography.X509Certificates;
using AlerterTranslator;

namespace AlerterScraper.Drinnies;

public class DrinniesChangeProcessor : IDrinniesChangeProcessor
{
    private readonly ITranslationService _translationService;

    public DrinniesChangeProcessor(ITranslationService translationService)
    {
        _translationService = translationService;
    }

    public async Task<ChangeDocument> GetChanges(IEnumerable<Product> master, IEnumerable<Product> updated)
    {
        var newProducts = new List<ChangedProduct>();
        var backInStock = new List<ChangedProduct>();
        var masterVariants = master.SelectMany(x => x.Variants);

        foreach (var updatedProduct in updated)
        {
            foreach (var updatedVariant in updatedProduct.Variants)
            {
                var changedProduct = new ChangedProduct
                {
                    Name = updatedProduct.Name,
                    Description = updatedProduct.Description,
                    VariantDescription = updatedVariant.Description
                };
                var masterVariant = masterVariants.FirstOrDefault(x => x.Id == updatedVariant.Id);
                if (masterVariant == null)
                {
                    var translated = await _translationService.Translate(new[] { updatedProduct.Description, updatedVariant.Description });
                    newProducts.Add(changedProduct with { Description = translated.First(), VariantDescription = translated.Last() });
                }
                else
                {
                    if (!masterVariant.InStock && updatedVariant.InStock)
                    {
                        var translated = await _translationService.Translate(new[] { updatedProduct.Description, updatedVariant.Description });
                        newProducts.Add(changedProduct with { Description = translated.First(), VariantDescription = translated.Last() });
                    }
                }
            }
        }
        return new ChangeDocument { NewProducts = newProducts, BackInStock = backInStock };
    }
}
