using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request;
using SWD.PhoneStoreManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SWD.PhoneStoreManagement.Service.Implement
{
    public class AuthenService : IAuthenService
    {
        private readonly IAuthenRepository authenRepository;
        private IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AuthenService(IAuthenRepository authenRepository, IConfiguration configuration, IUserRepository userRepository)
        {
            this.authenRepository = authenRepository;
            this.configuration = configuration;
            this.userRepository = userRepository;
        }
        public async Task<string?> Login(LoginRequest loginRequest)
        {
            string existingEmail = await authenRepository.findByEmail(loginRequest.Email);
            if (existingEmail == null)
            {
                throw new Exception("tài khoản chưa kích hoạt");
            }
            User user = await authenRepository.findAccount(existingEmail);
            if (user.Otp != null)
            {
                throw new Exception("tài khoản chưa xác thực");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.PasswordHash, user.PasswordHash);
            if (isPasswordValid)
            {
                return await GenerateJwtToken(user);
            }
            return null;
        }

        public async Task<User> Register(RegisterRequest registerRequest)
        {

            string email = await authenRepository.findByEmail(registerRequest.Email);
            if (email != null)
            {
                throw new Exception("email đã được sử dụng");
            }
            User user = new User
            {
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                FullName = registerRequest.FullName,
                Address = registerRequest.Address,
                PhoneNumber = registerRequest.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.PasswordHash),
                Role = "Customer",
                CreatedAt = DateTime.Now,
                Otp = GenerateOtp(),
            };
            var addUser = await authenRepository.AddUser(user);
            SendOtpEmail(user.Email, user.Otp);
            return addUser;
        }
        public async Task<User> GetUserById(int userID)
        {
            return await userRepository.GetUserById(userID);
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["jwt:Key"]);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim("userId", user.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("Role", user.Role), // Thêm RoleId vào claims
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Thêm JTI
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1), // Thời hạn hiệu lực của token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Trả về token dưới dạng chuỗi
        }

        // Forget Password with OTP
        public async Task ForgetPassword(string email)
        {
            var user = await authenRepository.FindByEmail(email);
            if (user == null)
            {
                throw new Exception("Email không tồn tại");
            }

            SendEmail(email); // Send OTP via email
        }

        // Verify OTP and Reset Password
        public async Task ResetPassword(string email, string newPassword, string confirmPassword)
        {
            var user = await authenRepository.FindByEmail(email);
            if (user == null || newPassword != confirmPassword)
            {
                throw new Exception("Vui lòng nhập lại tài khoản mật khẩu");
            }

            // Update password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await authenRepository.UpdateUser(user);
        }

        // Generate a 6-digit OTP
        private string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // Generates a number between 100000 and 999999
        }

        // Send OTP via email
        private void SendOtpEmail(string email, string otp)
        {
            var smtpClient = new SmtpClient(configuration["SMTP:Host"], int.Parse(configuration["SMTP:Port"]))
            {
                Credentials = new NetworkCredential(configuration["SMTP:Username"], configuration["SMTP:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration["SMTP:From"]),
                Subject = "Your Password Reset OTP",
                Body = $"Your OTP for password reset is: {otp}. It is valid for 5 minutes.",
                IsBodyHtml = false,
            };
            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }

        private void SendEmail(string email)
        {
            string link = "https://github.com/hoangbaohuy/PhoneManagementASPDOTNET";
            var smtpClient = new SmtpClient(configuration["SMTP:Host"], int.Parse(configuration["SMTP:Port"]))
            {
                Credentials = new NetworkCredential(configuration["SMTP:Username"], configuration["SMTP:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration["SMTP:From"]),
                Subject = "Your Password Reset OTP",
                Body = $"Your link for password reset is: {link}. It is valid for 5 minutes.",
                IsBodyHtml = false,
            };
            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }

        public async Task VerifyAccount(string email, string otp)
        {
            var user = await authenRepository.FindByEmail(email);
            if (user == null)
            {
                throw new Exception("Email không tồn tại");
            }
            if (user.Otp != otp)
            {
                throw new Exception("OTP không hợp lệ");
            }
            user.Otp = null;
            await authenRepository.UpdateUser(user);
        }
    }
}
