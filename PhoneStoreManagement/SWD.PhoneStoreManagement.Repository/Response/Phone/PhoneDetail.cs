using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.Phone
{
    public class PhoneDetail
    {
        public int PhoneId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? Image { get; set; }
        public string? Chipset { get; set; }
        public string? Gpu { get; set; }
        public string Color { get; set; }
        public int? WarrantyPeriod { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string OperatingSystem { get; set; }
        public int? Ram { get; set; }
        public int? Storage { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
