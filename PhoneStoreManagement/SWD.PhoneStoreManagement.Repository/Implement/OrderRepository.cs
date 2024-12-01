using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PhoneStoreDbContext _context;
        private readonly IMapper _mapper;
        public OrderRepository(PhoneStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetOrder>> GetAllOrdersAsync()
        {
            return _mapper.Map<List<GetOrder>>(await _context.Set<Order>().ToListAsync());
        }

        public async Task<GetOrder> GetOrderByIdAsync(int orderId)
        {
            return _mapper.Map<GetOrder>(await _context.Orders.AsNoTracking().Include(o => o.OrderDetails).ThenInclude(od => od.PhoneItems).FirstOrDefaultAsync(o => o.OrderId == orderId));
        }
       
        public async Task<IEnumerable<GetOrder>> GetOrderByUserIdAsync(int userId)
        {
            return _mapper.Map<List<GetOrder>>(await _context.Orders.AsNoTracking().Include(o => o.OrderDetails).ThenInclude(od => od.PhoneItems).Where(o => o.UserId == userId).ToListAsync());
        }
   
        public async Task<IEnumerable<GetOrderCf>> GetOrderByUserIdCfAsync(int userId)
        {
            return _mapper.Map<List<GetOrderCf>>(await _context.Orders.AsNoTracking().Include(o => o.OrderDetails).Where(o => o.UserId == userId).ToListAsync());
        }

        public async Task CreateOrdersAsync(Order order)
        {

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrdersAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }


    }
}
