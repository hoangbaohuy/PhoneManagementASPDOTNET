using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : Controller
    {

        private readonly IVnPayService _orderService;

        public VnPayController(IVnPayService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("proceed-vnpay-payment")]
        public async Task<IActionResult> ProceedVnPayPayment([FromBody] string orderId)
        {
            try
            {

                var paymentUrl = await _orderService.CreatePaymentUrlAsync(int.Parse(orderId));
                return Ok(new { paymentUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
