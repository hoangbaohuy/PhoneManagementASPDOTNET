using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Request.PhoneItem
{
    public class AddPhoneItem
    {
        public int PhoneId { get; set; }
        public string SerialNumber { get; set; } = null!;
    }
}
