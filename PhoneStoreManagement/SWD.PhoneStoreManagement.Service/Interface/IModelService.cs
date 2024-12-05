using SWD.PhoneStoreManagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IModelService
    {
        Task<List<Model>> GetAllModelsAsync();
        Task<Model?> GetModelByIdAsync(int id);
        Task AddModelAsync(Model model);
        Task UpdateModelAsync(Model model);
        Task DeleteModelAsync(int id);
    }
}
