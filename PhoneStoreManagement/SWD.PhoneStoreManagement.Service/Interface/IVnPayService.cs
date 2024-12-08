using Microsoft.AspNetCore.Http;
using SWD.PhoneStoreManagement.Repository.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWD.PhoneStoreManagement.Service.Interface
{
    public interface IVnPayService
    {
        Task<String> CreatePaymentUrlAsync(int orderId);
        //string CreatePaymentUrl(int OrderId, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
