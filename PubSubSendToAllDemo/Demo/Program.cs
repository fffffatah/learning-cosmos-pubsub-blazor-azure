using Azure.Messaging.WebPubSub;
using System;

class Program
{
    public static async Task Main(string[] args)
    {
        string message = "";
        while (true)
        {
            Console.WriteLine("Message: ");
            message = Console.ReadLine();
            var connectionString = Environment.GetEnvironmentVariable("PUBSUB_ENDPOINT");
            var hub = "Hub";

            // Either generate the token or fetch it from server or fetch a temp one from the portal
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            await serviceClient.SendToAllAsync(message);
        }
    }
}