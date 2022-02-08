using Microsoft.EntityFrameworkCore;
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
        public async Task<bool> Add(EntityLayer.File entity)
        {
            _cosmosContext.Add(entity);
            return (await _cosmosContext.SaveChangesAsync() > 0);
        }

        public async Task<bool> Delete(Guid id)
        {
            var file = (from f in _cosmosContext.Files where f.Id == id select f).FirstOrDefault();
            _cosmosContext.Remove(file);
            return (await _cosmosContext.SaveChangesAsync() > 0);
        }

        public async Task<EntityLayer.File> Get(Guid id)
        {
            return await (from f in _cosmosContext.Files where f.Id == id select f).FirstOrDefaultAsync();
        }

        public async Task<List<EntityLayer.File>> GetAll()
        {
            return await _cosmosContext.Files.ToListAsync();
        }

        public async Task<bool> Update(EntityLayer.File entity)
        {
            _cosmosContext.Update(entity);
            return (await _cosmosContext.SaveChangesAsync() > 0);
        }
    }
}
