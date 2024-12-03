using AutoMapper;
using SWD.PhoneStoreManagement.Repository.Implement;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Implement
{
    public class OrderDetailsService : IOrderDetailsService
    {

        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository,IPhoneRepository phoneRepository, IMapper mapper)
        {
            _orderDetailsRepository = orderDetailsRepository;
            _phoneRepository = phoneRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _mapper = mapper;
        }
        public async Task DeleteOrderDetails(int orderDetailsId)
        {
            //await _orderDetailsRepository.DeleteOrderDetailsAsync(orderDetailsId);
        }
    }
}
