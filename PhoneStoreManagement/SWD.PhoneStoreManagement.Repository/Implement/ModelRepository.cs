using Microsoft.EntityFrameworkCore;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class ModelRepository : IModelRepository
    {
        private readonly PhoneStoreDbContext _context;

        public ModelRepository(PhoneStoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Model>> GetAllAsync()
        {
            return await _context.Models
                .Include(m => m.Phones).ToListAsync();
        }

        public async Task<Model?> GetByIdAsync(int id)
        {
            return await _context.Models
                .Include(m => m.Brand)
                .Include(m => m.Phones)
                .FirstOrDefaultAsync(m => m.ModelId == id);
        }

        public async Task AddAsync(Model model)
        {
            await _context.Models.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Model model)
        {
            _context.Models.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model != null)
            {
                _context.Models.Remove(model);
                await _context.SaveChangesAsync();
            }
        }
    }
}
