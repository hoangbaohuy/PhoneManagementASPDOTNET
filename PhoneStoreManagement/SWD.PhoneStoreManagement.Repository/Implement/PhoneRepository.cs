using Microsoft.EntityFrameworkCore;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Response.Phone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly PhoneStoreDbContext _context;

        public PhoneRepository(PhoneStoreDbContext context)
        {
            _context = context;
        }
        public async Task<Phone> GetPhoneByIdAsync(int phoneId)
        {
            return await _context.Set<Phone>().FindAsync(phoneId);
        }



        /// lấy chi tiết phone và phone item 
        public async Task<Phone> GetPhoneAndItemByIdAsync(int phoneId)
        {
            return await _context.Phones.AsNoTracking().Include(i => i.PhoneItems).FirstOrDefaultAsync(o => o.PhoneId == phoneId); ;
        }

        public async Task<IEnumerable<Phone>> GetAllPhonesAsync()
        {
            return await _context.Set<Phone>().Include(b => b.PhoneItems).ToListAsync();
        }


        public async Task UpdatePhonesAsync(Phone phone)

        {
            _context.Phones.Update(phone);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PhoneDetail>> GetAllPhonesWithDetailsAsync()
        {
            return await _context.Phones
                .Include(p => p.Model)
                .ThenInclude(m => m.Brand)
                .Select(p => new PhoneDetail
                {
                    PhoneId = p.PhoneId,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    Image = p.Image,
                    Chipset = p.Chipset,
                    Gpu = p.Gpu,
                    Color = p.Color,
                    WarrantyPeriod = p.WarrantyPeriod,
                    ModelId = p.ModelId,
                    ModelName = p.Model.ModelName,
                    ReleaseDate = p.Model.ReleaseDate,
                    OperatingSystem = p.Model.OperatingSystem,
                    Ram = p.Model.Ram,
                    Storage = p.Model.Storage,
                    BrandId = p.Model.BrandId,
                    BrandName = p.Model.Brand.BrandName
                })
                .ToListAsync();
        }

        public async Task UpdatePhoneAsync(Phone phone)
        {
            _context.Phones.Update(phone);
            await _context.SaveChangesAsync();
        }
        public async Task AddPhoneAsync(Phone phone)
        {
            await _context.Phones.AddAsync(phone);
            await _context.SaveChangesAsync();
        }
        public async Task<Model?> GetModelByIdAsync(int modelId)
        {
            return await _context.Models.FindAsync(modelId);
        }
    }
}
