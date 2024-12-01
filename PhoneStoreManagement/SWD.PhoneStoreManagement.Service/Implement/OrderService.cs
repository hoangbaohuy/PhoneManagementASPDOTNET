using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Implement;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request.Order;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.OrderDetail;
using SWD.PhoneStoreManagement.Repository.Response.Phone;
using SWD.PhoneStoreManagement.Repository.Response.PhoneItem;
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
            var Order = await _orderRepository.GetOrderByIdAsync(orderId);
            foreach (var itemCf in Order.OrderDetails)
            {
                var phone = await _phoneRepository.GetPhoneByIdAsync(itemCf.PhoneId);
                if (phone == null)
                {
                    throw new Exception($"Phone with ID {itemCf.PhoneId} don't exit.");
                }
                itemCf.Image = phone.Image;
            }
            return Order;
        }

        public async Task<IEnumerable<GetOrder>> GetOrderByUserIdAsync(int useId)
        {
            var ListOrder = await _orderRepository.GetOrderByUserIdAsync(useId);
            foreach (var item in ListOrder)
            {
                foreach (var itemCf in item.OrderDetails)
                {
                    var phone = await _phoneRepository.GetPhoneByIdAsync(itemCf.PhoneId);
                    if (phone == null)
                    {
                        throw new Exception($"Phone with ID {itemCf.PhoneId} don't exit.");
                    }
                    itemCf.Image = phone.Image;
                }
            }
            return ListOrder;
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
                    if(phone == null)
                    {
                        throw new Exception($"Phone with ID {itemCf.PhoneId} don't exit.");
                    }
                    itemCf.img = phone.Image;
                    itemCf.PhoneName = phone.Description;
                }
            }
            return ListOrder;
        }
        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
   
            var userOrders = await _orderRepository.GetOrderByUserIdAsync(createOrder.UserId);

      
            var existingOrder = userOrders.FirstOrDefault(order => order.Status == "Pending");

            if (existingOrder != null)
            {

                foreach (var newItem in createOrder.OrderDetails)
                {
          
                    var existingDetail = existingOrder.OrderDetails.FirstOrDefault(detail => detail.PhoneId == newItem.PhoneId);
                    var phone = await _phoneRepository.GetPhoneByIdAsync(newItem.PhoneId);
                    if (existingDetail != null)
                    {
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
                var newOrder = _mapper.Map<Order>(createOrder);
                newOrder.OrderDate = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmount = 0;

                foreach (var item in newOrder.OrderDetails)
                {
                    var phone = await _phoneRepository.GetPhoneAndItemByIdAsync(item.PhoneId);
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
                    
                    //foreach (var itemphone in phone.PhoneItems)
                    //{
                    //    if (itemphone.Status == "Pending")
                    //    {
                    //        for (int i = 0;i < item.Quantity; i++)
                    //        {
                    //            item.PhoneItems.Add(itemphone);
                                 
                    //        }
                    //    }

                    //}
                }

                if (newOrder.OrderDetails == null || newOrder.OrderDetails.Count == 0)
                {
                    throw new Exception("Your order does not contain any products.");
                }

                await _orderRepository.CreateOrdersAsync(newOrder);
            }
        }

        public async Task DoneOrderAsync(int orderId)
        {
            // sau khi done order nay thi
            // so quantity cua dienthoai giam 
            // status cua Phoneitem = sold
            var userOrders = await _orderRepository.GetOrderByIdAsync(orderId);

            if (userOrders.Status == "Pending")
            {
                if(userOrders.OrderDetails == null || userOrders.OrderDetails.Count == 0)
                {
                    throw new Exception($"Your shopping cart is empty."); 
                }
                foreach (var item in userOrders.OrderDetails)
                {

                    var phone = _mapper.Map<Getphone>(await _phoneRepository.GetPhoneAndItemByIdAsync(item.PhoneId));
                 
                    if (phone == null )
                    {
                        throw new Exception($"Phone with ID {item.PhoneId} not found.");
                    }

                    if (item.Quantity > phone.StockQuantity)
                    {
                        throw new Exception($"Phone with ID {item.PhoneId} is out of stock.");
                    }

                    foreach (var itemphone in phone.PhoneItems)
                    {
                        if (itemphone.Status == "Pending")
                        {
                            for (int i = 0; i < item.Quantity; i++)
                            {
                                var mappedPhoneItem = _mapper.Map<GetPhoneItem>(itemphone);
                                mappedPhoneItem.PhoneId = phone.PhoneId;
                                mappedPhoneItem.Status = "sold";
                                mappedPhoneItem.DatePurchased = DateTime.Now;
                                mappedPhoneItem.ExpiryDate = DateTime.Now.AddDays(phone.WarrantyPeriod ?? 0);
                                item.PhoneItems.Add(mappedPhoneItem);
                            }
                        }

                    }
                    phone.StockQuantity -= item.Quantity;
              
                    userOrders.Status = "Completed";
                    
                }
                var mappedOrder = _mapper.Map<Order>(userOrders);
                await _orderRepository.UpdateOrdersAsync(mappedOrder);
            }

        }

        //clear order details : one click
        //delete order  : two click
        public async Task DeleteOrder(int orderId)
        {

            var userOrders = await _orderRepository.GetOrderByIdAsync(orderId);

            if (userOrders == null)
                throw new Exception($"Order with ID {orderId} not found.");


            foreach (var item in userOrders.OrderDetails.ToList()) 
            {
                var mappedOrderDetails = _mapper.Map<OrderDetail>(item);
                await _orderDetailsRepository.DeleteOrderDetails(mappedOrderDetails);
            }
            if(userOrders.OrderDetails.Count == 0)
            {
                var mappedOrder = _mapper.Map<Order>(userOrders);
                await _orderRepository.DeleteOrder(mappedOrder);
            }

        }

        public async Task warrantyOrderByCustomer(int orderId, string code)
        {
            // check theo mã điện thoại  -> từ đơn hàng , người dùng 
            var userOrders = await _orderRepository.GetOrderByIdAsync(orderId);
            var now = DateTime.Now;
            foreach (var item in userOrders.OrderDetails)
            {
                foreach(var phoneItem in item.PhoneItems)
                {
                    if (phoneItem.SerialNumber == code)
                    {
           
                        if (phoneItem.Status == "sold" && phoneItem.DatePurchased <= now && now <= phoneItem.ExpiryDate)
                        {
                            phoneItem.Status = "warranty";
                        } else
                        {
                            throw new Exception($"this phone was Expired Warranty ");
                        }
                    }else
                    {
                        throw new Exception($"Wrong code ");
                    }
                }
            }
            var mappedOrder = _mapper.Map<Order>(userOrders);
            await _orderRepository.UpdateOrdersAsync(mappedOrder);

        }
        // status = Warranty, Under Warranty ,Warranty Expired,Warranty Claimed
        public async Task warrantyOrderByShopOwner(int orderId, string code,string status)
        {
             
            var userOrders = await _orderRepository.GetOrderByIdAsync(orderId);

            foreach (var item in userOrders.OrderDetails)
            {
                foreach (var phoneItem in item.PhoneItems)
                {
                    if (phoneItem.SerialNumber == code )
                    {
                        phoneItem.Status = status;
                    }
                }
            }
            var mappedOrder = _mapper.Map<Order>(userOrders);
            await _orderRepository.UpdateOrdersAsync(mappedOrder);

        }



    }
}
