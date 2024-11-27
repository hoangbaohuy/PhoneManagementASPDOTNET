using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
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

        public PhoneService(IPhoneRepository phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        public async Task<Phone> GetPhoneByIdAsync(int phoneId)
        {
            return await _phoneRepository.GetPhoneByIdAsync(phoneId);
        }

        public async Task<IEnumerable<Phone>> GetAllPhonesAsync()
        {
            return await _phoneRepository.GetAllPhonesAsync();
        }

    }
}
