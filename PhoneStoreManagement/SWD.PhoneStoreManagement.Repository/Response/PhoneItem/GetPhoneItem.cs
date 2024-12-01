using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.PhoneItem
{
    public class GetPhoneItem
    {
        public int PhoneItemId { get; set; }
        public int PhoneId { get; set; }
        public string SerialNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime DateImported { get; set; }

        public DateTime? DatePurchased { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }
}
