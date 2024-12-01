using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SWD.PhoneStoreManagement.Repository.Request.PhoneItem;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class PhoneItemController : ODataController
    {
        private readonly IPhoneItemService _phoneItemService;

        public PhoneItemController(IPhoneItemService phoneItemService)
        {
            _phoneItemService = phoneItemService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPhoneItem([FromBody] AddPhoneItem request)
        {
            try
            {
                await _phoneItemService.AddPhoneItemAsync(request);
                return Ok(new { Message = "PhoneItem added successfully, stock quantity updated." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
