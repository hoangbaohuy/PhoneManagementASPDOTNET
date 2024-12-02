using AutoMapper;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request.Phone;
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

        public async Task UpdatePhoneAsync(int phoneId, UpdatePhone updatedPhoneData)
        {
            // Retrieve the existing phone
            var existingPhone = await _phoneRepository.GetPhoneByIdAsync(phoneId);
            if (existingPhone == null)
            {
                throw new ArgumentException("Phone with the given ID does not exist.");
            }

            // Update fields only if the new values are provided
            if (updatedPhoneData.ModelId.HasValue)
                existingPhone.ModelId = updatedPhoneData.ModelId.Value;

            if (!string.IsNullOrWhiteSpace(updatedPhoneData.Description))
                existingPhone.Description = updatedPhoneData.Description;

            if (updatedPhoneData.Price.HasValue)
                existingPhone.Price = updatedPhoneData.Price.Value;

            if (updatedPhoneData.StockQuantity.HasValue)
                existingPhone.StockQuantity = updatedPhoneData.StockQuantity.Value;

            if (!string.IsNullOrWhiteSpace(updatedPhoneData.Image))
                existingPhone.Image = updatedPhoneData.Image;

            if (!string.IsNullOrWhiteSpace(updatedPhoneData.Chipset))
                existingPhone.Chipset = updatedPhoneData.Chipset;

            if (!string.IsNullOrWhiteSpace(updatedPhoneData.Gpu))
                existingPhone.Gpu = updatedPhoneData.Gpu;

            if (!string.IsNullOrWhiteSpace(updatedPhoneData.Color))
                existingPhone.Color = updatedPhoneData.Color;

            if (updatedPhoneData.WarrantyPeriod.HasValue)
                existingPhone.WarrantyPeriod = updatedPhoneData.WarrantyPeriod.Value;

            // Save updated phone
            await _phoneRepository.UpdatePhoneAsync(existingPhone);
        }
    }
}
