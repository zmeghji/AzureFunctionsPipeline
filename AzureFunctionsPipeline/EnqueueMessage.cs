using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionsPipeline
{
    public static class EnqueueJob
    {
        [FunctionName("EnqueueJob")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function, 
                "post", 
                Route = null
            )] HttpRequest req,
            [Queue("jobs")] IAsyncCollector<Job> jobQueue,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function started.");
            var job = JsonConvert.DeserializeObject < Job >(
                await new StreamReader(req.Body).ReadToEndAsync()
                );
            await jobQueue.AddAsync(job);
            log.LogInformation("C# HTTP trigger function complete.");
            return new OkResult();
        }
    }
}
