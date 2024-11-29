using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.User
{
    public class UserResponse
    {
        public int UserId { get; set; } // Required for identifying the user
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
