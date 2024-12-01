using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<GetOrder> GetOrderByIdAsync(int phoneId);
        Task<IEnumerable<GetOrder>> GetOrderByUserIdAsync(int userId);
        Task<IEnumerable<GetOrderCf>> GetOrderByUserIdCfAsync(int userId);
        Task<IEnumerable<GetOrder>> GetAllOrdersAsync();
        Task CreateOrdersAsync(Order corder);
        Task UpdateOrdersAsync(Order corder);
        Task DeleteOrder(Order order);
    }
}
