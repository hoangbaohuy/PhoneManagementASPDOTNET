using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Repository.Request.Model
{
    public class ModelDto
    {
        public int BrandId { get; set; }
        public string ModelName { get; set; } = null!;
        public string OperatingSystem { get; set; } = null!;
        public int? Ram { get; set; }
        public int? Storage { get; set; }
    }
}
