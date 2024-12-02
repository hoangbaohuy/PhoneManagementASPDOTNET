using SWD.PhoneStoreManagement.Repository.Request.PhoneItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IPhoneItemService
    {
        Task AddPhoneItemAsync(AddPhoneItem request);
    }
}
