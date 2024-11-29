using Microsoft.EntityFrameworkCore;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly PhoneStoreDbContext _context;

        public UserRepository(PhoneStoreDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(n => n.UserId == userId);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserResponse
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    Image = u.Image,
                    Address = u.Address,
                    PhoneNumber = u.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserResponse
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    Image = u.Image,
                    Address = u.Address,
                    PhoneNumber = u.PhoneNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUserIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
