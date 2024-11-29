using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Response.User;
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
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUserIdAsync(int userId);
        Task UpdateUserAsync(User user);
    }
}
