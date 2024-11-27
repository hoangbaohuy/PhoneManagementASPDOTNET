using SWD.PhoneStoreManagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Interface
{
    public interface IAuthenRepository
    {
        Task<User> AddUser(User user);
        Task<User> findAccount(string existingEmail);
        Task<string> findByEmail(string email);
        Task<User?> FindByEmail(string email);
        Task UpdateUser(User user);

    }
}
