using SWD.PhoneStoreManagement.Repository.Response.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.Order
{
    public class GetOrderPayment
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public virtual GetPayment? Payment { get; set; }
    }
}
