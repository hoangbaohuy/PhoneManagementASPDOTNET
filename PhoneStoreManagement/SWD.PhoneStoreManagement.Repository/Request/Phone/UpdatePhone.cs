using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Request.Phone
{
    public class UpdatePhone
    {
        public int? ModelId { get; set; } // Nullable to check if the value is provided
        public string? Description { get; set; } // Optional field
        public decimal? Price { get; set; } // Nullable to allow updates only if provided
        public int? StockQuantity { get; set; } // Nullable for partial updates
        public string? Image { get; set; } // Optional field
        public string? Chipset { get; set; } // Optional field
        public string? Gpu { get; set; } // Optional field
        public string? Color { get; set; } // Optional field
        public int? WarrantyPeriod { get; set; } // Nullable to allow updating warranty
    }
}
