using AutoMapper;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
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
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _phoneRepository;
        private readonly IMapper _mapper;

        public PhoneService(IPhoneRepository phoneRepository,IMapper mapper)
        {
            _phoneRepository = phoneRepository;
            _mapper = mapper;
        }

        public async Task<Phone> GetPhoneByIdAsync(int phoneId)
        {
            return await _phoneRepository.GetPhoneByIdAsync(phoneId);
        }

        public async Task<IEnumerable<Phone>> GetAllPhonesAsync()
        {
            return await _phoneRepository.GetAllPhonesAsync();
        }

        public async Task<Getphone> GetPhoneandPhoneQItemByIdAsync(int phoneId)
        {
            return _mapper.Map<Getphone>(await _phoneRepository.GetPhoneAndItemByIdAsync(phoneId));
        }

        public async Task<IEnumerable<PhoneDetail>> GetAllPhoneDetailsAsync()
        {
            return await _phoneRepository.GetAllPhonesWithDetailsAsync();
        }
    }
}
