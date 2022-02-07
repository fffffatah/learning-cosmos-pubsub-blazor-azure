using EntityLayer;
using BusinessLogic;

namespace UMSDemo.Data
{
    public class UserService
    {
        public List<User> GetUsers()
        {
            return UserLogic.GetAll();
        }

        public User Get(Guid id)
        {
            return UserLogic.Get(id);
        }

        public bool Update(User user)
        {
            return UserLogic.Update(user);
        }

        public bool AddUser(User user)
        {
            return UserLogic.Add(user);
        }

        public bool DeleteUser(Guid id)
        {
            return UserLogic.Delete(id);
        }
    }
}
