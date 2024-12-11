using AutoMapper;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Implement;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request.Payment;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.Payment;
using SWD.PhoneStoreManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentrRepository _paymentrRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentrRepository paymentrRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _paymentrRepository = paymentrRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task CreatePaymentAsync(int orderdId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderdId);


            if (order == null)
            {
                throw new Exception($"Phone with ID {order.OrderId} don't exit.");
            }

            var payment = new Payment()
            {
                OrderId = order.OrderId,
                AmountPaid = order.TotalAmount,
                PaymentDate = order.OrderDate,
                PaymentMethod = "VNpay"
            };

            await _paymentrRepository.CreatePaymentAsync(payment);
        }

        public async Task<IEnumerable<GetPayment>> GetAllPayMentsAsync()
        {
            return await _paymentrRepository.GetAllPayMentsAsync();
        }

        public async Task<IEnumerable<GetPayment>> GetPayMentByOrderIdAsync(int orderId)
        {
            return await _paymentrRepository.GetPayMentByOrderIdAsync(orderId);
        }

        public async Task<IEnumerable<GetOrderPayment>> GetOrdersWithByUserPayment(int userId)
        {
            var ListOrder = await _orderRepository.GetOrdersWithByUserPayment(userId);

            return ListOrder;
        }
    }
}
