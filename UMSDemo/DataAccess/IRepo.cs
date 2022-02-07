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
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(ID id);
        List<T> GetAll();
        T Get(ID id);
    }
}
