using Azure.Messaging.WebPubSub;
using Microsoft.Azure.WebPubSub.Common;
using Websocket.Client;
using System;

class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Enter Access Uri: ");
        string? inputUri = Console.ReadLine();
        var url = new Uri(inputUri);
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