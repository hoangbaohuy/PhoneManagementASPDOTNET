using System;
using System.Collections.Generic;

namespace SWD.PhoneStoreManagement.Repository.Entity;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int PhoneId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Phone Phone { get; set; } = null!;

    public virtual ICollection<PhoneItem> PhoneItems { get; set; } = new List<PhoneItem>();
}
