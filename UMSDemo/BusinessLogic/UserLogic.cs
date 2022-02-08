using EntityLayer;
using DataAccess;

namespace BusinessLogic
{
    public class UserLogic
    {
        public static async Task<bool> Add(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            return await Repository.UserDataAccess().Add(user);
        }
        public static async Task<bool> Update(User user)
        {
            user.UpdatedAt = DateTime.Now;
            return await Repository.UserDataAccess().Update(user);
        }
        public static async Task<List<User>> GetAll()
        {
            return await Repository.UserDataAccess().GetAll();
        }
        public static async Task<User> Get(Guid id)
        {
            return await Repository.UserDataAccess().Get(id);
        }
        public static async Task<bool> Delete(Guid id)
        {
            return await Repository.UserDataAccess().Delete(id);
        }
    }
}