using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsPipeline
{
    public static class CreateJsonBlob
    {
        [FunctionName("CreateJsonBlob")]
        public async static Task Run(
            [QueueTrigger(
                "jobs", 
                Connection = "AzureWebJobsStorage")
            ] string myQueueItem, 
            [Blob("jobs/{rand-guid}.json")] TextWriter blobTextWriter,
            IBinder binder,
            ILogger log)
        {
            log.LogInformation($"Entered Function For the Queue Item: {myQueueItem}");
            blobTextWriter.Write(myQueueItem);

            string blob2Path = "jobs/job_" + Guid.NewGuid().ToString() + ".json";

            var blob2Writer = await binder.BindAsync<TextWriter>(
                new BlobAttribute(blobPath: blob2Path)
                );
            //outputBlob.
            blob2Writer.Write(myQueueItem);
            log.LogInformation($"Exiting Function For the Queue Item: {myQueueItem}");
        }
    }
}
