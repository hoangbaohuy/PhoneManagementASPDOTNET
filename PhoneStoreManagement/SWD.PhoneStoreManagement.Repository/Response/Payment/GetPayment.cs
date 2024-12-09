using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.Payment
{
    public class GetPayment
    {
        public int PaymentId { get; set; }

        public int OrderId { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public decimal AmountPaid { get; set; }

    }
}
