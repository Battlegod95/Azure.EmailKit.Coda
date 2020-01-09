using Mail.Data;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Newtonsoft.Json;
using System;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=itsmonti;AccountKey=jlG4ynn4gEaH32F3dgPpA6QTxNEc0dTj5k/3vyR6fx8Z2Zy24m0ORvFMVehqMvoCYPArjaFaSiNEVlgi+LjTCA==;EndpointSuffix=core.windows.net";

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();
            var emailobject = new Email();

            while (true) 
            { 
            
                Console.WriteLine("inserisci la mail");
                emailobject.email = Console.ReadLine();

                Console.WriteLine("inserisci l'oggetto");
                emailobject.oggetto = Console.ReadLine();

                Console.WriteLine("inserisci il testo");
                emailobject.testo = Console.ReadLine();

                Console.WriteLine("L'email che hai usato è: {0}, l'oggetto della mail è: {1}, e il testo è {2}", emailobject.email, emailobject.oggetto, emailobject.testo);

                var Json = JsonConvert.SerializeObject(emailobject);
                Console.WriteLine(Json);

                CloudQueueMessage messageJson = new CloudQueueMessage(Json);
                queue.AddMessage(messageJson);
            }

        }
    }
}
