using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataAccess;

namespace DataAccess
{
    public class UserRepo : IRepo<User, Guid>
    {
        CosmosContext _cosmosContext;
        public UserRepo(CosmosContext cosmosContext)
        {
            _cosmosContext = cosmosContext;
        }
        public bool Add(User entity)
        {
            _cosmosContext.Add(entity);
            return (_cosmosContext.SaveChanges() > 0);
        }

        public bool Delete(Guid id)
        {
            var user = (from u in _cosmosContext.Users where u.Id == id select u).FirstOrDefault();
            _cosmosContext.Remove(user);
            return (_cosmosContext.SaveChanges() > 0);
        }

        public User Get(Guid id)
        {
            return (from u in _cosmosContext.Users where u.Id == id select u).FirstOrDefault();
        }

        public List<User> GetAll()
        {
            return _cosmosContext.Users.ToList();
        }

        public bool Update(User entity)
        {
            _cosmosContext.Update(entity);
            return (_cosmosContext.SaveChanges() > 0);
        }
    }
}
