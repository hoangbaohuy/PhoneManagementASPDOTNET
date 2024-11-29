using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Request.User
{
    public class UserUpdateRequest
    {
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
