using EntityLayer;
using BusinessLogic;

namespace UMSDemo.Data
{
    public class UserService
    {
        public async Task<List<User>> GetUsers()
        {
            return await UserLogic.GetAll();
        }

        public async Task<User> Get(Guid id)
        {
            return await UserLogic.Get(id);
        }

        public async Task<bool> Update(User user)
        {
            return await UserLogic.Update(user);
        }

        public async Task<bool> AddUser(User user)
        {
            return await UserLogic.Add(user);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            return await UserLogic.Delete(id);
        }
    }
}
