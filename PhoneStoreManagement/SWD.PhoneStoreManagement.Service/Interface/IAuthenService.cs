using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IAuthenService
    {
        Task<string?> Login(LoginRequest loginRequest);
        Task<User> Register(RegisterRequest registerRequest);
        Task<User> GetUserById(int userID);
        Task ForgetPassword(string email);
        Task ResetPassword(string email, string newPassword, string confirmPassword);
        Task VerifyAccount(string email, string otp);
        Task<User> RegisterMobile(RegisterRequest registerRequest);
    }
}
