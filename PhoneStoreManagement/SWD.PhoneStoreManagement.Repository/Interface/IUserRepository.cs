using SWD.PhoneStoreManagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByUserNameAsync(string userName);
        Task AddUserAsync(User user);
        Task<User> GetUserById(int userId);
    }
}
