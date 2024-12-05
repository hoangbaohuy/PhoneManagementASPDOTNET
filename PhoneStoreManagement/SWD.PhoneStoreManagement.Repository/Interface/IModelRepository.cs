using SWD.PhoneStoreManagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Interface
{
    public interface IModelRepository
    {
        Task<List<Model>> GetAllAsync();
        Task<Model?> GetByIdAsync(int id);
        Task AddAsync(Model model);
        Task UpdateAsync(Model model);
        Task DeleteAsync(int id);
    }
}
