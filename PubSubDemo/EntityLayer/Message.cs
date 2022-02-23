using System.Text.Json.Serialization;

namespace EntityLayer
{
    public class Message
    {
        public string? Id { get; set; }
        public string? Text { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
    }
}