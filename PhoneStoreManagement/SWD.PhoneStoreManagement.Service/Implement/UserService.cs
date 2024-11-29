using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Response.User;
using SWD.PhoneStoreManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<bool> UpdateUserAsync(int userId, User updateRequest)
        {
            // Retrieve the existing user
            var existingUser = await _userRepository.GetUserByUserIdAsync(userId);
            if (existingUser == null)
            {
                return false; // User not found
            }

            // Update only non-null fields
            if (!string.IsNullOrEmpty(updateRequest.FullName))
            {
                existingUser.FullName = updateRequest.FullName;
            }
            if (!string.IsNullOrEmpty(updateRequest.Image))
            {
                existingUser.Image = updateRequest.Image;
            }
            if (!string.IsNullOrEmpty(updateRequest.Address))
            {
                existingUser.Address = updateRequest.Address;
            }
            if (!string.IsNullOrEmpty(updateRequest.PhoneNumber))
            {
                existingUser.PhoneNumber = updateRequest.PhoneNumber;
            }

            // Save the updated user
            await _userRepository.UpdateUserAsync(existingUser);
            return true;
        }
    }
}
