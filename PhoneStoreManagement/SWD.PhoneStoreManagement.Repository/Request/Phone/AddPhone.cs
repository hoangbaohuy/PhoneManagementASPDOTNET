using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Request.Phone
{
    public class AddPhone
    {
        [Required]
        public int ModelId { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "StockQuantity cannot be negative.")]
        public int StockQuantity { get; set; }

        public string? Image { get; set; }

        public string? Chipset { get; set; }

        public string? Gpu { get; set; }

        [Required]
        public string Color { get; set; } = string.Empty;

        [Range(0, 60, ErrorMessage = "WarrantyPeriod must be between 0 and 60 months.")]
        public int? WarrantyPeriod { get; set; }
    }

}
