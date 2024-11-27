using System;
using System.Collections.Generic;

namespace SWD.PhoneStoreManagement.Repository.Entity;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int OrderId { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public decimal AmountPaid { get; set; }

    public virtual Order Order { get; set; } = null!;
}
