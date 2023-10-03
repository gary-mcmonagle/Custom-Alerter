using System;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AlerterFunctions.Email;

public class SendEmail
{
    [FunctionName("SendEmail")]

    public async Task Run([QueueTrigger("emails", Connection = "AzureWebJobsStorage")] string myQueueItem, ILogger log)
    {
        // This code retrieves your connection string from an environment variable.
        string connectionString = Environment.GetEnvironmentVariable("EMAIL_CONNECTION_STRING");
        var emailClient = new EmailClient(connectionString);
        await emailClient.SendAsync(
            WaitUntil.Completed,
            new EmailMessage(
                Environment.GetEnvironmentVariable("EMAIL_FROM"),
                Environment.GetEnvironmentVariable("EMAIL_TO"),
                new EmailContent("subject") { Html = myQueueItem }),
            default);
    }
}
