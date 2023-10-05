using AlerterScraper.Drinnies;
using HandlebarsDotNet;

namespace AlerterEmailGeneration;

public class DrinniesEmailGenerator : IDrinniesEmailGenerator
{
    public string? GenerateEmail(ChangeDocument changes)
    {
        string source =
            @"<div class=""entry"">
            <h1>{{title}}</h1>
            <div class=""body"">
                {{#each changes}} {{Id}} {{/each}}
            </div>
            </div>";
        var template = Handlebars.Compile(source);

        var data = new
        {
            title = "My new post",
            changes = new List<object> { new { Id = "GARY" }, new { Id = "TEST" } }
        };

        var result = template(data);
        return result;
    }
}
