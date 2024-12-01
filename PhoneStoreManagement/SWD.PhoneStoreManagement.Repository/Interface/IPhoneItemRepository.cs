using SWD.PhoneStoreManagement.Repository.Request.PhoneItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Interface
{
    public interface IPhoneItemRepository
    {
        Task AddPhoneItemAsync(AddPhoneItem request);
    }
}
