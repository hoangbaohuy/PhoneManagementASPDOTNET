using Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly PhoneDbContext _context;

        public PhoneRepository(PhoneDbContext context)
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
