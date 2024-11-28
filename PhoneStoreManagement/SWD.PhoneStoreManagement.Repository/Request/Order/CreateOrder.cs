using SWD.PhoneStoreManagement.Repository.Request.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Request.Order
{
    public class CreateOrder
    {

        public int UserId { get; set; }

        public virtual ICollection<CreateOrderDetail> OrderDetails { get; set; } = [];


    }
}
