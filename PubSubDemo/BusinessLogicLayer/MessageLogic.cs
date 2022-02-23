using EntityLayer;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class MessageLogic
    {
        public static async Task<bool> AddMessageAsync(Message message)
        {
            message.Id = Guid.NewGuid().ToString();
            return await Factory.MessageDataAccess().CreateAsync(message);
        }
        public static async Task<List<Message>> GetMessagesAsync(string sender, string receiver)
        {
            return await Factory.MessageDataAccess().GetMessagesAsync(sender, receiver);
        }
    }
}