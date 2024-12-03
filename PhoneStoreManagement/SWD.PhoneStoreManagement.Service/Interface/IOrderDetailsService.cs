using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IOrderDetailsService
    {
        Task DeleteOrderDetails(int orderDetailsId);
    }
}
