using AlerterScraper.Drinnies;
using HandlebarsDotNet;

namespace AlerterEmailGeneration;

public class DrinniesEmailGenerator : IDrinniesEmailGenerator
{
    public string? GenerateEmail(ChangeDocument changes)
    {
        if (changes.NewProducts.Count() == 0 && changes.BackInStock.Count() == 0)
        {
            return null;
        }
        string source =
            @"<div class=""entry"">
            <h1>{{title}}</h1>
            <div class=""body"">
                <h2>Back in stock</h2>
                {{#each backInStock}}
                <ul>
                    <li>{{Name}}</li>
                    <li>{{Description}}</li>
                    <li>{{VariantDescription}}</li>
                </ul>
                {{/each}}
                <h2>New Products</h2>

                {{#each newProducts}}
                    <ul>
                        <li>{{Name}}</li>
                        <li>{{Description}}</li>
                        <li>{{VariantDescription}}</li>
                    </ul>
                {{/each}}
            </div>
            </div>";
        var template = Handlebars.Compile(source);

        var data = new
        {
            title = "Drinnies Update",
            backInStock = changes.BackInStock,
            newProducts = changes.NewProducts
        };

        var result = template(data);
        return result;
    }
}
