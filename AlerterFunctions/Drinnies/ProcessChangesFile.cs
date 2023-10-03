using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AlerterFunctions.Drinnies;

public class ProcessChangesFile
{
    [FunctionName("ProcessChangeFile")]
    [return: Queue("emails")]

    public string Run(
        [BlobTrigger("drinnies/changes.json", Connection = "AzureWebJobsStorage")] string changesBlob,
        ILogger log)
    {
        return changesBlob;
    }
}
