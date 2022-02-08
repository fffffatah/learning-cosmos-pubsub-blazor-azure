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
        public static async Task<bool> Add(EntityLayer.File file)
        {
            return await Repository.FileDataAccess().Add(file);
        }
        public static async Task<bool> Update(EntityLayer.File file)
        {
            return await Repository.FileDataAccess().Update(file);
        }
        public static async Task<List<EntityLayer.File>> GetAll()
        {
            return await Repository.FileDataAccess().GetAll();
        }
        public static async Task<EntityLayer.File> Get(Guid id)
        {
            return await Repository.FileDataAccess().Get(id);
        }
        public static async Task<bool> Delete(Guid id)
        {
            return await Repository.FileDataAccess().Delete(id);
        }
    }
}
