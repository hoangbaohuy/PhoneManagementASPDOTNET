using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Interface
{
    public interface IOrderDetailsRepository
    {
        //Task<GetOrder> GetOrderDetailByIdAsync(int phoneId);
        //Task<IEnumerable<GetOrder>> GetAllOrderDetailsAsync();
        Task DeleteOrderDetails(OrderDetail order);
    }
}
