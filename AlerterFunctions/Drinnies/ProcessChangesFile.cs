using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlerterEmailGeneration;
using AlerterFunctions.Email;
using AlerterScraper.Drinnies;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AlerterFunctions.Drinnies;

public class ProcessChangesFile
{
    private readonly IDrinniesEmailGenerator _drinniesEmailGenerator;

    public ProcessChangesFile(IDrinniesEmailGenerator drinniesEmailGenerator)
    {
        _drinniesEmailGenerator = drinniesEmailGenerator;
    }

    [FunctionName("ProcessChangeFile")]
    [return: Queue("emails")]

    public QueueEmailMessage Run(
        [BlobTrigger("drinnies/changes.json", Connection = "AzureWebJobsStorage")] string changesBlob,
        ILogger log)
    {
        var changes = JsonConvert.DeserializeObject<ChangeDocument>(changesBlob);
        var email = _drinniesEmailGenerator.GenerateEmail(changes);
        if (email is not null)
        {
            return new QueueEmailMessage
            {
                Subject = "Drinnies Changes",
                Body = email
            };
        }
        return null;
    }
}
