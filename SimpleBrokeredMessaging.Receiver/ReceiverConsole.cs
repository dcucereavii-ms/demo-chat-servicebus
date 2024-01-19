using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

namespace SimpleBrokeredMessaging.Receiver
{
    internal class ReceiverConsole
    {
        //ToDo: Enter a valid Service Bus connection string
        static string ConnectionString = "Endpoint=sb://messaging-demo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wtCy236mj0ZsseiKh5XBZMINNvce2Ae11+ASbEicU4E=";
        static string QueueName = "demoqueue";


        static async Task Main(string[] args)
        {
            // Create a service bus client
            var client = new ServiceBusClient(ConnectionString);

            // Create a service bus receiver
            var receiver = client.CreateReceiver(QueueName);


            // Receive the messages
            Console.WriteLine("Receive messages...");
            while (true)
            {
                var message = await receiver.ReceiveMessageAsync();

                if (message != null)
                {
                    Console.Write(message.Body.ToString());

                    // Complete the message
                    await receiver.CompleteMessageAsync(message);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("All messages received.");
                    break;
                }
            }

            // Close the receiver
            await receiver.CloseAsync();
        }
    }
}
