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
            return await _context.Set<Phone>().ToListAsync();
        }


        public async Task UpdatePhonesAsync(Phone phone)

        {
            _context.Phones.Update(phone);
            await _context.SaveChangesAsync();
        }
    }
}
