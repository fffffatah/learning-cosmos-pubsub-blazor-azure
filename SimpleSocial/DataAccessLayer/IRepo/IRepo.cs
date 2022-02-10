using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepo
{
    public interface IRepo<T, ID>
    {
        //Common Data Access Methods
        Task<bool> Create(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(ID id);
        Task<T> Get(ID id);
        Task<List<T>> GetAll();
        //User Data Access Methods
        Task<T> GetByUsername(string username);
    }
}
