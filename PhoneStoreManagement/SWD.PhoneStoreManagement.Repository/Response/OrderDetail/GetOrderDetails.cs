using SWD.PhoneStoreManagement.Repository.Response.PhoneItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.OrderDetail
{
    public class GetOrderDetails
    {
        public int OrderDetailId { get; set; }
        public int PhoneId { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual ICollection<GetPhoneItem> PhoneItems { get; set; } = [];
    }
}
