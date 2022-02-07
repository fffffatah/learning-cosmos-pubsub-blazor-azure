using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class FileLogic
    {
        public static bool Add(EntityLayer.File file)
        {
            return Repository.FileDataAccess().Add(file);
        }
        public static bool Update(EntityLayer.File file)
        {
            return Repository.FileDataAccess().Update(file);
        }
        public static List<EntityLayer.File> GetAll()
        {
            return Repository.FileDataAccess().GetAll();
        }
        public static EntityLayer.File Get(Guid id)
        {
            return Repository.FileDataAccess().Get(id);
        }
        public static bool Delete(Guid id)
        {
            return Repository.FileDataAccess().Delete(id);
        }
    }
}
