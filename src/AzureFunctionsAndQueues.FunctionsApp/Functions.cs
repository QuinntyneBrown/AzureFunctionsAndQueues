using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;

namespace AzureFunctionsAndQueues.FunctionsApp
{
    public class Functions
    {
        private CloudQueue _queue;

        public Functions()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                Environment.GetEnvironmentVariable("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            _queue = queueClient.GetQueueReference("orderedqueue");
            _queue.CreateIfNotExists();
        }

        [FunctionName("Get")]
        public IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get")] HttpRequest req
            )
        {
            CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            _queue.AddMessage(message);
            return new OkObjectResult("Message Submitted!");
        }

        [FunctionName("ProcessMessage")]
        public void ProcessMessage(
            [QueueTrigger("orderedqueue", Connection = "StorageConnectionString")]string message)
        {
            Console.WriteLine(message);
        }
    }
}
