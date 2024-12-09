using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request.Payment;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class PaymentrRepository : IPaymentrRepository
    {
        private readonly PhoneStoreDbContext _context;
        private readonly IMapper _mapper;
        public PaymentrRepository(PhoneStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreatePaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetPayment>> GetAllPayMentsAsync()
        {
            return _mapper.Map<List<GetPayment>>(await _context.Set<Payment>().ToListAsync());
        }

        public async Task<IEnumerable<GetPayment>> GetPayMentByOrderIdAsync(int orderId)
        {

                var payments = await _context.Payments
                    .AsNoTracking()
                    .Where(o => o.OrderId == orderId)
                    .ToListAsync();
                
                return _mapper.Map<List<GetPayment>>(payments);
            
        }
    }
}
