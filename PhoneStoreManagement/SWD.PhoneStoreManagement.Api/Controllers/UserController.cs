using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.User;
using SWD.PhoneStoreManagement.Repository.Response.User;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    public class UsersController : ODataController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("edit-profile/{userId}")]
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UserUpdateRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            var userToUpdate = new User
            {
                FullName = updateRequest.FullName,
                Image = updateRequest.Image,
                Address = updateRequest.Address,
                PhoneNumber = updateRequest.PhoneNumber
            };

            bool isUpdated = await _userService.UpdateUserAsync(userId, userToUpdate);
            if (isUpdated)
            {
                return Ok(new { message = "User updated successfully." });
            }
            return NotFound(new { message = "User not found." });
        }
    }
}
