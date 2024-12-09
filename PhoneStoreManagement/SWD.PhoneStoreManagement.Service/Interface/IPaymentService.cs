using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.Order;
using SWD.PhoneStoreManagement.Repository.Request.Payment;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<GetPayment>> GetPayMentByOrderIdAsync(int orderId);
        Task<IEnumerable<GetPayment>> GetAllPayMentsAsync();
        Task CreatePaymentAsync(int orderId);
    }
}
