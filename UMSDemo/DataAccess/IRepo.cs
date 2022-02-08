using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace DataAccess
{
    public interface IRepo<T, ID>
    {
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(ID id);
        Task<List<T>> GetAll();
        Task<T> Get(ID id);
    }
}
