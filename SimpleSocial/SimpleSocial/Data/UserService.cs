using BusinessLogicLayer;
using EntityLayer;

namespace SimpleSocial.Data
{
    public class UserService
    {
        public async Task<bool> UpdateUserAsync(User user)
        {
            return await UserLogic.Update(user);
        }
        public async Task<bool> CreateUserAsync(UserRegistrationModel user)
        {
            return await UserLogic.Create(user);
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await UserLogic.GetAll();
        }
        public async Task<User> GetUserAsync(Guid id)
        {
            return await UserLogic.Get(id);

        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await UserLogic.Delete(id);
        }
    }
}