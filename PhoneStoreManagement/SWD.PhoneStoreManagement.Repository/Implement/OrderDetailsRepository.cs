﻿using AutoMapper;
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
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly PhoneStoreDbContext _context;
        private readonly IMapper _mapper;

        public OrderDetailsRepository(PhoneStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetOrderDetails> GetOrderByIdAsync(int orderdetailId)
        {
            return _mapper.Map<GetOrderDetails>(await _context.Orders.AsNoTracking().Include(o => o.OrderDetails).ThenInclude(od => od.PhoneItems).FirstOrDefaultAsync(o => o.OrderId == orderdetailId));
        }
        public async Task DeleteOrderDetails(OrderDetail order)
        {
            _context.OrderDetails.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
