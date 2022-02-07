using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FileRepo : IRepo<EntityLayer.File, Guid>
    {
        CosmosContext _cosmosContext;
        public FileRepo(CosmosContext cosmosContext)
        {
            _cosmosContext = cosmosContext;
        }
        public bool Add(EntityLayer.File entity)
        {
            _cosmosContext.Add(entity);
            return (_cosmosContext.SaveChanges() > 0);
        }

        public bool Delete(Guid id)
        {
            var file = (from f in _cosmosContext.Files where f.Id == id select f).FirstOrDefault();
            _cosmosContext.Remove(file);
            return (_cosmosContext.SaveChanges() > 0);
        }

        public EntityLayer.File Get(Guid id)
        {
            return (from f in _cosmosContext.Files where f.Id == id select f).FirstOrDefault();
        }

        public List<EntityLayer.File> GetAll()
        {
            return _cosmosContext.Files.ToList();
        }

        public bool Update(EntityLayer.File entity)
        {
            _cosmosContext.Update(entity);
            return (_cosmosContext.SaveChanges() > 0);
        }
    }
}
