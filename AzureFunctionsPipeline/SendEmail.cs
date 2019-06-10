using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace AzureFunctionsPipeline
{
    public static class SendEmail
    {
        [FunctionName("SendEmail")]
        public static void Run(
            [BlobTrigger(
                "jobs/{name}", 
                Connection = "AzureWebJobsStorage")
            ] Stream myBlob, 
            [SendGrid(ApiKey ="SendGridApiKey")] out SendGridMessage message,
            string name, 
            ILogger log)
        {
            log.LogInformation($"Entered Function For Blob Name:{name} \n Size: {myBlob.Length} Bytes");
            var fromEmailAddress = new EmailAddress("test@example.com");
            var toEmailAddress = new EmailAddress("test@example.com");
            message = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, "test", "this is a test", "<strong>this is a test</strong>");

            log.LogInformation($"Exiting Function For Blob Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
