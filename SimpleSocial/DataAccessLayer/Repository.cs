using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.IRepo;
using DataAccessLayer.Repo;
using EntityLayer;

namespace DataAccessLayer
{
    public class Repository
    {
        static CosmosContext _db;
        static Repository()
        {
            _db = new CosmosContext();
        }
        public static IRepo<User, Guid> UserDataAccess()
        {
            return new UserRepo(_db);
        }
    }
}
