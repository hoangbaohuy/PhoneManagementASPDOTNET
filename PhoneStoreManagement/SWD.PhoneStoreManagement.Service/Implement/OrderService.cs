using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Implement;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request.Order;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.OrderDetail;
using SWD.PhoneStoreManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IPhoneRepository phoneRepository,IOrderDetailsRepository orderDetailsRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _phoneRepository = phoneRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<GetOrder>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<GetOrder> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<GetOrder>> GetOrderByUserIdAsync(int useId)
        {
            return await _orderRepository.GetOrderByUserIdAsync(useId);
        }
        // fix lại 
        //public async Task CreateOrderAsync(CreateOrder createOrder)
        //{
        //    var listOrder = await _orderRepository.GetOrderByUserIdAsync(createOrder.UserId);
        //    foreach (var exititem in listOrder)
        //    {
        //        if (exititem.Status == "InProcess")
        //        {

        //        }
        //    }


        //    var order = _mapper.Map<Order>(createOrder);
        //    List<int> lackphone = new List<int>();
        //    order.OrderDate = DateTime.Now;
        //    order.Status = "InProcess";

        //    foreach (var item in order.OrderDetails)
        //    {
        //        var phone = await _phoneRepository.GetPhoneByIdAsync(item.PhoneId);
        //        if (phone == null)
        //        {
        //            throw new Exception($"Phone with ID {item.PhoneId} not found.");
        //        }

        //        if (item.Quantity > phone.StockQuantity)
        //        {
        //            lackphone.Add(item.PhoneId);
        //        }

        //        item.UnitPrice = phone.Price * item.Quantity;
        //        order.TotalAmount += item.UnitPrice;
        //    }
        //    if (order.OrderDetails == null || order.OrderDetails.Count == 0)
        //    {
        //        throw new Exception($"You haven't added your phone yet.");
        //    }

        //    if (lackphone.Count > 0)
        //    {
        //        throw new Exception($"Phones with IDs {string.Join(", ", lackphone)} are out of stock.");
        //    }
        //    await _orderRepository.CreateOrdersAsync(order);
        //}
        public async Task<IEnumerable<GetOrderCf>> GetOrderByUserIdCfAsync(int useId)
        {
            var ListOrder = await _orderRepository.GetOrderByUserIdCfAsync(useId);
            foreach (var item in ListOrder) 
            {
                foreach (var itemCf in item.OrderDetails)
                {
                    var phone = await _phoneRepository.GetPhoneByIdAsync(itemCf.PhoneId);
                    itemCf.img = phone.Image;
                }
            }
            return ListOrder;
        }
        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
   
            var userOrders = await _orderRepository.GetOrderByUserIdAsync(createOrder.UserId);

      
            var existingOrder = userOrders.FirstOrDefault(order => order.Status == "InProcess");

            if (existingOrder != null)
            {

                foreach (var newItem in createOrder.OrderDetails)
                {
          
                    var existingDetail = existingOrder.OrderDetails.FirstOrDefault(detail => detail.PhoneId == newItem.PhoneId);
                    var phone = await _phoneRepository.GetPhoneByIdAsync(newItem.PhoneId);
                    if (existingDetail != null)
                    {
                        // Nếu tồn tại, cập nhật số lượng
                        if (newItem.Quantity > phone.StockQuantity)
                        {
                            throw new Exception($"Phone with ID {newItem.PhoneId} is out of stock.");
                        }
                        existingDetail.Quantity = newItem.Quantity;
                        existingDetail.UnitPrice = existingDetail.Quantity * phone.Price;
                        if (existingDetail.Quantity == 0)
                        {
                            var ordetails = _mapper.Map<OrderDetail>(existingDetail);
                            await _orderDetailsRepository.DeleteOrderDetails(ordetails);
                        }
                    }
                    else
                    {

                        
                        if (phone == null)
                        {
                            throw new Exception($"Phone with ID {newItem.PhoneId} not found.");
                        }
                        if (newItem.Quantity > phone.StockQuantity)
                        {
                            throw new Exception($"Phone with ID {newItem.PhoneId} is out of stock.");
                        }
                        existingOrder.OrderDetails.Add(new GetOrderDetails
                        {
                            PhoneId = newItem.PhoneId,
                            Quantity = newItem.Quantity,
                            UnitPrice = phone.Price * newItem.Quantity
                        });
                    }
                }

        
                existingOrder.TotalAmount = existingOrder.OrderDetails.Sum(detail => detail.UnitPrice);

                var exitdetails =  _mapper.Map<Order>(existingOrder);
                await _orderRepository.UpdateOrdersAsync(exitdetails);
            }
            else
            {
                // Nếu chưa có đơn hàng "InProcess", tạo mới đơn hàng
                var newOrder = _mapper.Map<Order>(createOrder);
                newOrder.OrderDate = DateTime.Now;
                newOrder.Status = "InProcess";
                newOrder.TotalAmount = 0;

                foreach (var item in newOrder.OrderDetails)
                {
                    var phone = await _phoneRepository.GetPhoneByIdAsync(item.PhoneId);
                    if (phone == null)
                    {
                        throw new Exception($"Phone with ID {item.PhoneId} not found.");
                    }

                    if (item.Quantity > phone.StockQuantity)
                    {
                        throw new Exception($"Phone with ID {item.PhoneId} is out of stock.");
                    }

                    item.UnitPrice = phone.Price;
                    newOrder.TotalAmount += item.UnitPrice * item.Quantity;
                }

                if (newOrder.OrderDetails == null || newOrder.OrderDetails.Count == 0)
                {
                    throw new Exception("Your order does not contain any products.");
                }

                await _orderRepository.CreateOrdersAsync(newOrder);
            }
        }

        public async Task DoneOrderAsync(CreateOrder createOrder)
        {
            
        }
    }
}
