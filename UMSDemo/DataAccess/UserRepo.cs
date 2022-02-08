using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UserRepo : IRepo<User, Guid>
    {
        CosmosContext _cosmosContext;
        public UserRepo(CosmosContext cosmosContext)
        {
            _cosmosContext = cosmosContext;
        }
        public async Task<bool> Add(User entity)
        {
            _cosmosContext.Add(entity);
            return (await _cosmosContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = (from u in _cosmosContext.Users where u.Id == id select u).FirstOrDefault();
            _cosmosContext.Remove(user);
            return (await _cosmosContext.SaveChangesAsync() > 0);
        }

        public async Task<User> Get(Guid id)
        {
            return await (from u in _cosmosContext.Users where u.Id == id select u).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _cosmosContext.Users.ToListAsync();
        }

        public async Task<bool> Update(User entity)
        {
            _cosmosContext.Update(entity);
            return (await _cosmosContext.SaveChangesAsync() > 0);
        }
    }
}
