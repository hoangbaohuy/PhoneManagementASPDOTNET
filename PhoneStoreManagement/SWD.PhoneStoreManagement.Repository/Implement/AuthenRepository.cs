using Microsoft.EntityFrameworkCore;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly PhoneStoreDbContext _context;

        public AuthenRepository(PhoneStoreDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<User> findAccount(string existingEmail)
        {
            var user = _context.Users.FirstOrDefault(n => n.Email == existingEmail);
            return user;
        }

        public async Task<string> findByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(n => n.Email == email);
            if (user != null)
            {
                return user.Email;
            }

            return null;
        }

        // Tìm người dùng qua email
        public async Task<User?> FindByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Cập nhật thông tin người dùng
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
