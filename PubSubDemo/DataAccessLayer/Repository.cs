using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class Repository : IRepository<Message>
    {
        protected CosmosContext _db;
        public Repository(CosmosContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Message message)
        {
            await _db.AddAsync(message);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<List<Message>> GetMessagesAsync(string sender, string receiver)
        {
            return await (from message in _db.Messages where message.Sender == sender && message.Receiver == receiver select message).ToListAsync();
        }
    }
}
