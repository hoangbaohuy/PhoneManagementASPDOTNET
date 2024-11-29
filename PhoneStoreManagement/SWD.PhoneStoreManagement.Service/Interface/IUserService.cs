using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int userId, User updateRequest);
    }
}
