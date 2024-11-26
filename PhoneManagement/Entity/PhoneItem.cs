using System;
using System.Collections.Generic;

namespace Entity;

public partial class PhoneItem
{
    public int PhoneItemId { get; set; }

    public int PhoneId { get; set; }

    public int? OrderDetailId { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime DateImported { get; set; }

    public DateTime? DatePurchased { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }

    public virtual Phone Phone { get; set; } = null!;
}
