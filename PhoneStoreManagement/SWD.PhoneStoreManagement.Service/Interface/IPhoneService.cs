using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.Phone;
using SWD.PhoneStoreManagement.Repository.Response.Phone;
using SWD.PhoneStoreManagement.Repository.Response.PhoneItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IPhoneService
    {
        Task<Phone> GetPhoneByIdAsync(int phoneId);
        Task<Getphone> GetPhoneandPhoneQItemByIdAsync(int phoneId);
        Task<IEnumerable<Phone>> GetAllPhonesAsync();
        Task<IEnumerable<PhoneDetail>> GetAllPhoneDetailsAsync();
        Task UpdatePhoneAsync(int phoneId, UpdatePhone updatedPhoneData);
    }
}
