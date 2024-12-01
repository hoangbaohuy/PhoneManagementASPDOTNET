using SWD.PhoneStoreManagement.Repository.Request.Order;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IOrderService
    {
        Task<GetOrder> GetOrderByIdAsync(int orderId);

        Task<IEnumerable<GetOrder>> GetOrderByUserIdAsync(int useId);

        Task<IEnumerable<GetOrderCf>> GetOrderByUserIdCfAsync(int useId);
        Task<IEnumerable<GetOrder>> GetAllOrdersAsync();
        Task CreateOrderAsync(CreateOrder createOrder);
        Task DoneOrderAsync(int orderId);
        Task warrantyOrderByCustomer(int orderId, string code);
        Task warrantyOrderByShopOwner(int orderId, string code,string status);
        Task DeleteOrder(int orderId);



    }
}
