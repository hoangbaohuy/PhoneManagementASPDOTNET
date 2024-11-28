using SWD.PhoneStoreManagement.Repository.Response.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.Order
{
    public class GetOrderCf
    {
        public int OrderId { get; set; }
        public virtual ICollection<OrderProfile> OrderDetails { get; set; } = [];
    }
}
