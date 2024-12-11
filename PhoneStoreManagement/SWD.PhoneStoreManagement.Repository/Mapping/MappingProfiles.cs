using AutoMapper;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.Order;
using SWD.PhoneStoreManagement.Repository.Request.OrderDetail;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.OrderDetail;
using SWD.PhoneStoreManagement.Repository.Response.Payment;
using SWD.PhoneStoreManagement.Repository.Response.Phone;
using SWD.PhoneStoreManagement.Repository.Response.PhoneItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            #region Order
            CreateMap<CreateOrder, Order>().ReverseMap();
            CreateMap<Order, GetOrder>().ReverseMap();
            CreateMap<Order, GetOrderCf>().ReverseMap();
            CreateMap<Order, GetOrderPayment>().ReverseMap();
            #endregion

            #region Order detail
            CreateMap<CreateOrderDetail, OrderDetail>();
            CreateMap<GetOrderDetails, OrderDetail>().ReverseMap();
            CreateMap<OrderDetail, OrderProfile>().ReverseMap();
            #endregion

            #region phone 
            CreateMap<Phone, Getphone>().ReverseMap();
            #endregion

            #region phone item
            CreateMap<PhoneItem, GetPhoneItem>().ReverseMap();
            #endregion

            #region Payment
            CreateMap<Payment, GetPayment>().ReverseMap();
            #endregion
        }
    }
}
