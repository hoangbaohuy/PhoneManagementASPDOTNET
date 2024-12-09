using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : Controller
    {

        private readonly IVnPayService _orderService;
        private readonly IPaymentService _paymentService;
        
        public VnPayController(IVnPayService orderService,IPaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var phones = await _paymentService.GetAllPayMentsAsync();
            return Ok(phones);
        }

        [HttpGet("order-payment/{orderid}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentByOrderId(int orderid)
        {
            var phone = await _paymentService.GetPayMentByOrderIdAsync(orderid);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(phone);
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


        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreatePayPayment( int orderId)
        {
            try
            {
                await _paymentService.CreatePaymentAsync(orderId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }




        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallBack()
        {
            try
            {
                var response = _orderService.PaymentExecute(HttpContext.Request.Query);

                if (response == null || !int.TryParse(response.OrderId?.ToString(), out int orderId)) 
                    return Redirect("https://localhost:7295/payment-failure");

                if (response.VnPayResponseCode == "00")
                {
                    await _paymentService.CreatePaymentAsync(orderId);
                    return Redirect("https://localhost:7295/payment-success");
                }
              

                return Redirect("https://localhost:7295/payment-failure");
            }
            catch (Exception)
            {
                return Redirect("https://localhost:7295/payment-failure");
            }
            // return Json(new { response });
        }
    }
}
