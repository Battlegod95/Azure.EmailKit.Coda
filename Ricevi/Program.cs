using Mail.Data;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace EmailReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "";

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            int second = 1000;
            while (true)
            {
                var queueMessage = queue.GetMessage();
                if (queueMessage != null)
                {
                    Email message = JsonConvert.DeserializeObject<Email>(queueMessage.AsString);
                    SendMail.Send(message, "","", "");
                    Console.WriteLine(queueMessage.AsString);
                    queue.DeleteMessage(queueMessage);
                    second = 1000;
                }
                else
                {
                    Console.WriteLine("nessun messaggio sulla coda");
                    second = second * 2;
                    if (second == 32000)
                    {
                        second = 1000;
                    }
                }
                Thread.Sleep(second);
            }
        }
    }
}
