using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPhoneService
    {
        Task<Phone> GetPhoneByIdAsync(int phoneId);
        Task<IEnumerable<Phone>> GetAllPhonesAsync();
    }
}
