using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace DataAccessLayer
{
    public class Factory
    {
        static CosmosContext _db;
        public Factory()
        {
            _db = new CosmosContext();
        }
        public static IRepository<Message> MessageDataAccess()
        {
            return new Repository(_db);
        }
    }
}
