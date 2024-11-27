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

        public async Task<IEnumerable<Phone>> GetAllPhonesAsync()
        {
            return await _context.Set<Phone>().ToListAsync();
        }
    }
}
