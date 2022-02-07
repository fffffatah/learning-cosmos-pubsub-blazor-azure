using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using EntityLayer;

namespace DataAccess
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
        public static IRepo<EntityLayer.File, Guid> FileDataAccess()
        {
            return new FileRepo(_db);
        }
    }
}
