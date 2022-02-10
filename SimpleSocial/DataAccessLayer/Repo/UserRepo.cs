using DataAccessLayer.IRepo;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repo
{
    public class UserRepo : IRepo<User, Guid>
    {
        protected readonly CosmosContext _db;
        public UserRepo(CosmosContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(User item)
        {
            item.Id = Guid.NewGuid();
            await _db.AddAsync(item);
            return (await _db.SaveChangesAsync() > 0);
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await (from u in _db.Users where u.Id == id select u).FirstOrDefaultAsync();
            if (user == null) return false;
            _db.Remove(user);
            return (await _db.SaveChangesAsync() > 0);
        }

        public async Task<User> Get(Guid id)
        {
            return await (from u in _db.Users where u.Id == id select u).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await (from u in _db.Users where u.Username==username select u).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(User item)
        {
            _db.Update(item);
            return (await _db.SaveChangesAsync() > 0);
        }
    }
}
