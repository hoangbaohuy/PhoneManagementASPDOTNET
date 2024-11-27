using Microsoft.AspNetCore.Mvc;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenService;

        public AuthenController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] SWD.PhoneStoreManagement.Repository.Request.RegisterRequest registerRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _authenService.Register(registerRequest);

                return Ok(new { message = "Đăng ký thành công", user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SWD.PhoneStoreManagement.Repository.Request.LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.PasswordHash))
            {
                return BadRequest("Email or password cannot be empty.");
            }

            try
            {
                var token = await _authenService.Login(loginRequest);
                if (token == null)
                {
                    return Unauthorized("Tài khoạn không xác thực.");
                }
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] SWD.PhoneStoreManagement.Repository.Request.ForgetPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email cannot be empty.");
            }

            try
            {
                await _authenService.ForgetPassword(request.Email);
                return Ok(new { message = "OTP has been sent to your email." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] SWD.PhoneStoreManagement.Repository.Request.ResetPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.NewPassword) || string.IsNullOrEmpty(request.ConfirmPassword))
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                await _authenService.ResetPassword(request.Email, request.NewPassword, request.ConfirmPassword);
                return Ok(new { message = "Password has been reset successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("verify-account")]
        public async Task<IActionResult> VerifyAccount([FromBody] SWD.PhoneStoreManagement.Repository.Request.VerifyAccountRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Otp))
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                await _authenService.VerifyAccount(request.Email, request.Otp);
                return Ok(new { message = "Password has been reset successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
