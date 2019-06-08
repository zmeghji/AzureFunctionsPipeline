using AzureFunctionsPipeline;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace TriggerPipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("local.settings.json");
            var configuration = configurationBuilder.Build();
            var url = configuration.GetSection("WebHookURL").Value;

            var client = new HttpClient();
            var job = new Job { HostName = "machine 1", SoftwareName = "7-zip" };
            var content = new StringContent(JsonConvert.SerializeObject(job));

            while (true)
            {
                Console.WriteLine("Press enter to send request");
                Console.ReadLine();
                var resp = client.PostAsync(url, content).Result;
            }

            Console.WriteLine("Hello World!");
        }
    }
}
