using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPhoneRepository
    {
        Task<Phone> GetPhoneByIdAsync(int phoneId);
        Task<IEnumerable<Phone>> GetAllPhonesAsync();
    }
}
