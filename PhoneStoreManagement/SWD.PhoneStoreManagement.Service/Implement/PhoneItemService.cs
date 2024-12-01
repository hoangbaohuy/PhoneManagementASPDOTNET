using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request.PhoneItem;
using SWD.PhoneStoreManagement.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Implement
{
    public class PhoneItemService : IPhoneItemService
    {
        private readonly IPhoneItemRepository _phoneItemRepository;

        public PhoneItemService(IPhoneItemRepository phoneItemRepository)
        {
            _phoneItemRepository = phoneItemRepository;
        }

        public async Task AddPhoneItemAsync(AddPhoneItem request)
        {
            await _phoneItemRepository.AddPhoneItemAsync(request);
        }
    }
}
