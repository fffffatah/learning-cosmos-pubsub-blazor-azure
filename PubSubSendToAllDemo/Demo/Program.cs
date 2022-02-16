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
            var connectionString = "Endpoint=https://fffffatah.webpubsub.azure.com;AccessKey=4rxRv695jTc0HY8l2eRYigyo1RoYJKgIlGlW+adGNVU=;Version=1.0;";
            var hub = "Hub";

            // Either generate the token or fetch it from server or fetch a temp one from the portal
            var serviceClient = new WebPubSubServiceClient(connectionString, hub);
            await serviceClient.SendToAllAsync(message);
        }
    }
}