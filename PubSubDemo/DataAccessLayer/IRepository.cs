using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;

namespace DataAccessLayer
{
    public interface IRepository<T>
    {
        Task<List<T>> GetMessagesAsync(string sender, string receiver);
        Task<bool> CreateAsync(T message);
    }
}
