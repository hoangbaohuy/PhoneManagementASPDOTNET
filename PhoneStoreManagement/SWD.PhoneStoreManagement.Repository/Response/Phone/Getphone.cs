using SWD.PhoneStoreManagement.Repository.Response.PhoneItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Response.Phone
{
    public class Getphone
    {
        public int PhoneId { get; set; }

        public int ModelId { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? Image { get; set; }

        public string? Chipset { get; set; }

        public string? Gpu { get; set; }

        public string Color { get; set; } = null!;

        public int? WarrantyPeriod { get; set; }

        public virtual ICollection<GetPhoneItem> PhoneItems { get; set; } = [];
    }
}
