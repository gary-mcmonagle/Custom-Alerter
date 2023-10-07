using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AlerterScraper.Drinnies;

namespace Custom.Alerter.Functions.Drinnies;
public class GetDrinniesProducts
{
    private readonly IDrinniesProductScraper _drinniesProductScraper;

    public GetDrinniesProducts(IDrinniesProductScraper drinniesProductScraper)
    {
        _drinniesProductScraper = drinniesProductScraper;
    }

    [FunctionName("GetDrinniesProducts")]
    public async Task Run(
        [TimerTrigger("0 15 * * *")] TimerInfo myTimer,
        [Blob("drinnies/update.json", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream updateStream,

        ILogger log)
    {
        var products = await _drinniesProductScraper.GetProducts();
        await updateStream.WriteAsync(System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(products)));
    }
}
