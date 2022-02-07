using EntityLayer;
using DataAccess;

namespace BusinessLogic
{
    public class UserLogic
    {
        public static bool Add(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            return Repository.UserDataAccess().Add(user);
        }
        public static bool Update(User user)
        {
            user.UpdatedAt = DateTime.Now;
            return Repository.UserDataAccess().Update(user);
        }
        public static List<User> GetAll()
        {
            return Repository.UserDataAccess().GetAll();
        }
        public static User Get(Guid id)
        {
            return Repository.UserDataAccess().Get(id);
        }
        public static bool Delete(Guid id)
        {
            return Repository.UserDataAccess().Delete(id);
        }
    }
}