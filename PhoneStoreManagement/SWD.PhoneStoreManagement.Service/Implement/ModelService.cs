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
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;

        public ModelService(IModelRepository modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public async Task<List<Model>> GetAllModelsAsync()
        {
            return await _modelRepository.GetAllAsync();
        }

        public async Task<Model?> GetModelByIdAsync(int id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task AddModelAsync(Model model)
        {
            await _modelRepository.AddAsync(model);
        }

        public async Task UpdateModelAsync(Model model)
        {
            await _modelRepository.UpdateAsync(model);
        }

        public async Task DeleteModelAsync(int id)
        {
            await _modelRepository.DeleteAsync(id);
        }
    }
}
