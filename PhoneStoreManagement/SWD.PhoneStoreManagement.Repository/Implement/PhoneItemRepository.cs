using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Interface;
using SWD.PhoneStoreManagement.Repository.Request.PhoneItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Implement
{
    public class PhoneItemRepository : IPhoneItemRepository
    {
        private readonly PhoneStoreDbContext _context;

        public PhoneItemRepository(PhoneStoreDbContext context)
        {
            _context = context;
        }

        public async Task AddPhoneItemAsync(AddPhoneItem request)
        {
            // Check if the phone exists
            var phone = await _context.Phones.FindAsync(request.PhoneId);
            if (phone == null)
            {
                throw new KeyNotFoundException($"Phone with ID {request.PhoneId} not found.");
            }

            // Create a new PhoneItem
            var phoneItem = new PhoneItem
            {
                PhoneId = request.PhoneId,
                SerialNumber = request.SerialNumber,
                Status = "Available", // Set default status
                DateImported = DateTime.Now
            };

            // Add the PhoneItem
            await _context.PhoneItems.AddAsync(phoneItem);

            // Increase the StockQuantity of the Phone
            phone.StockQuantity++;

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
