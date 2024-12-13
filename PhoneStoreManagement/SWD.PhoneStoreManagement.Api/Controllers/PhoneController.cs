using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.Phone;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class PhoneController : ODataController
    {
        private readonly IPhoneService _phoneService;

        public PhoneController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        // GET: api/phones
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Phone>>> GetPhones()
        {
            var phones = await _phoneService.GetAllPhonesAsync();
            return Ok(phones);
        }

        // GET: api/phones/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Phone>> GetPhone(int id)
        {
            var phone = await _phoneService.GetPhoneByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(phone);
        }

        // GET: api/phones/{id}
        [HttpGet("details/{id}")]
        public async Task<ActionResult<Phone>> GetPhonedetails(int id)
        {
            var phone = await _phoneService.GetPhoneandPhoneQItemByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return Ok(phone);
        }

        [HttpGet("details")]
        [EnableQuery]
        public async Task<IActionResult> GetAllPhones()
        {
            var phones = await _phoneService.GetAllPhoneDetailsAsync();
            return Ok(phones);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhone(int id, [FromBody] UpdatePhone updatedPhoneData)
        {
            try
            {
                await _phoneService.UpdatePhoneAsync(id, updatedPhoneData);
                return Ok(new { Message = "Phone updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPhone([FromBody] AddPhone addPhoneRequest)
        {
            try
            {
                // Map the AddPhone request to the Phone entity
                var phone = new Phone
                {
                    ModelId = addPhoneRequest.ModelId,
                    Description = addPhoneRequest.Description,
                    Price = addPhoneRequest.Price,
                    StockQuantity = addPhoneRequest.StockQuantity,
                    Image = addPhoneRequest.Image,
                    Chipset = addPhoneRequest.Chipset,
                    Gpu = addPhoneRequest.Gpu,
                    Color = addPhoneRequest.Color,
                    WarrantyPeriod = addPhoneRequest.WarrantyPeriod
                };

                // Pass the mapped entity to the service
                await _phoneService.AddPhoneAsync(phone);
                return Ok(new { Message = "Phone added successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred.", Details = ex.Message });
            }
        }

    }
}
