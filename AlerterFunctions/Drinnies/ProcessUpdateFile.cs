using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlerterScraper.Drinnies;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;

namespace Alerter.Functions.Drinnies;
public class ProcessUpdateFile
{
    private readonly IDrinniesChangeProcessor _drinniesChangeProcessor;

    public ProcessUpdateFile(IDrinniesChangeProcessor drinniesChangeProcessor)
    {
        _drinniesChangeProcessor = drinniesChangeProcessor;
    }

    [FunctionName("ProcessUpdateFile")]
    public async Task Run(
        [BlobTrigger("drinnies/update.json", Connection = "AzureWebJobsStorage")] string updateBlob,
        [Blob("drinnies/master.json", FileAccess.Read, Connection = "AzureWebJobsStorage")] string masterBlob,
        [Blob("drinnies/changes.json", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream changeStream,
        [Blob("drinnies/master.json", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream masterStream,
        ILogger log)
    {
        var updateProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(updateBlob);
        var masterProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(masterBlob ?? "") ?? new List<Product>();
        var changeSet = await _drinniesChangeProcessor.GetChanges(masterProducts, updateProducts);
        await changeStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(changeSet)));
        await masterStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(updateProducts)));
    }
}
