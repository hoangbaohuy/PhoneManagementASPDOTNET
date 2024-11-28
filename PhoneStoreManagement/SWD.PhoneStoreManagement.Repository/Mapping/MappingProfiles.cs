using AutoMapper;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.Order;
using SWD.PhoneStoreManagement.Repository.Request.OrderDetail;
using SWD.PhoneStoreManagement.Repository.Response.Order;
using SWD.PhoneStoreManagement.Repository.Response.OrderDetail;
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
            CreateMap<CreateOrder, Order>();
            CreateMap<Order, GetOrder>().ReverseMap();
            #endregion

            #region Order detail
            CreateMap<CreateOrderDetail, OrderDetail>();
            CreateMap<GetOrderDetails, OrderDetail>().ReverseMap();
            #endregion

            #region phone item
            CreateMap<PhoneItem, GetPhoneItem>();
            #endregion
        }
    }
}
