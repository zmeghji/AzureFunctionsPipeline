using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionsPipeline
{
    public class Job
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string HostName { get; set; }
        public string SoftwareName { get; set; }
    }
}
