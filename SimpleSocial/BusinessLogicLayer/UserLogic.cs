using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataAccessLayer;
using AutoMapper;

namespace BusinessLogicLayer
{
    public class UserLogic
    {
        public static async Task<User> Get(Guid id)
        {
            return await Repository.UserDataAccess().Get(id);
        }
        public static async Task<List<User>> GetAll()
        {
            return await Repository.UserDataAccess().GetAll();
        }
        public static async Task<bool> Create(UserRegistrationModel user)
        {
            user.Pass = BCrypt.Net.BCrypt.HashPassword(user.Pass, BCrypt.Net.BCrypt.GenerateSalt());
            var config = new MapperConfiguration(c =>
            {
                //Map "UserRegistrationModel" to "User"
                c.CreateMap<UserRegistrationModel, User>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<User>(user);
            data.Id = Guid.NewGuid();
            data.IsAdmin = false;
            return await Repository.UserDataAccess().Create(data);
        }
        public static async Task<bool> Update(User user)
        {
            return await Repository.UserDataAccess().Update(user);
        }
        public static async Task<bool> Delete(Guid id)
        {
            return await Repository.UserDataAccess().Delete(id);
        }
    }
}
