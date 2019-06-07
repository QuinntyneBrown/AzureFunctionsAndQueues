using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionsAndQueues.FunctionsApp
{
    public class Functions
    {
        private CloudQueue _queue;

        public Functions()
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    Environment.GetEnvironmentVariable("StorageConnectionString"));
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                _queue = queueClient.GetQueueReference("Ordered");
                _queue.CreateIfNotExists();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);                
            }
        }

        [FunctionName("Get")]
        public IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get")] HttpRequest req
            )
        {
            try
            {
                CloudQueueMessage message = new CloudQueueMessage("Hello, World");
                _queue.AddMessage(message);
                return new OkObjectResult("Ordererd Queue");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
