using System;
using System.Collections.Generic;

namespace SWD.PhoneStoreManagement.Repository.Entity;

public partial class Phone
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

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<PhoneItem> PhoneItems { get; set; } = new List<PhoneItem>();
}
