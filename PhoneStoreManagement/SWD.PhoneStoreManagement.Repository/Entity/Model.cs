using System;
using System.Collections.Generic;

namespace SWD.PhoneStoreManagement.Repository.Entity;

public partial class Model
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = null!;

    public int BrandId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string OperatingSystem { get; set; } = null!;

    public int? Ram { get; set; }

    public int? Storage { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
