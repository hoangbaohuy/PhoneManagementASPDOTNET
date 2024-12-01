using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.Order;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class OrderController : ODataController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/phones
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var phones = await _orderService.GetAllOrdersAsync();
            return Ok(phones);
        }

        // GET: api/phones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var phone = await _orderService.GetOrderByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(phone);
        }

        [HttpGet("get-order-by-id-user/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUser(int id)
        {
            var phone = await _orderService.GetOrderByUserIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(phone);
        }

        [HttpGet("get-order-by-id-user-cf/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderByUserCf(int id)
        {
            var phone = await _orderService.GetOrderByUserIdCfAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(phone);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrder order)
        {
            await _orderService.CreateOrderAsync(order);

            return Ok();
        }

        [HttpPut("done/{orderId}")]
        public async Task<ActionResult<Order>> DoneOrder(int orderId)
        {
            await _orderService.DoneOrderAsync(orderId);

            return Ok();
        }

        [HttpPut("warranty_cus/{orderId}")]
        public async Task<ActionResult<Order>> warrantyCustomerOrder(int orderId, string code)
        {
            await _orderService.warrantyOrderByCustomer(orderId,code);

            return Ok();
        }

        [HttpPut("warranty_ad/{orderId}")]
        public async Task<ActionResult<Order>> warrantyShopOrder(int orderId, string code,string status)
        {
            await _orderService.warrantyOrderByShopOwner(orderId,code,status);

            return Ok();
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(int orderId)
        {
            await _orderService.DeleteOrder(orderId);

            return Ok();
        }
    }
}
