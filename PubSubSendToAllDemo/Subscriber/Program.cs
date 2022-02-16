using Azure.Messaging.WebPubSub;
using Websocket.Client;
using System;

class Program
{
    public static async Task Main(string[] args)
    {
        var connectionString = "Endpoint=https://fffffatah.webpubsub.azure.com;AccessKey=4rxRv695jTc0HY8l2eRYigyo1RoYJKgIlGlW+adGNVU=;Version=1.0;";
        var hub = "Hub";

        // Either generate the URL or fetch it from server or fetch a temp one from the portal
        var serviceClient = new WebPubSubServiceClient(connectionString, hub);
        var url = serviceClient.GetClientAccessUri();
        using (var client = new WebsocketClient(url))
        {
            // Disable the auto disconnect and reconnect because the sample would like the client to stay online even no data comes in
            client.ReconnectTimeout = null;
            client.MessageReceived.Subscribe(msg => Console.WriteLine($"Message received: {msg}"));
            await client.Start();
            Console.WriteLine("Connected.");
            Console.Read();
        }
    }
}