﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SWD.PhoneStoreManagement.Repository.Entity;
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
        public async Task<IActionResult> GetAllPhones()
        {
            var phones = await _phoneService.GetAllPhoneDetailsAsync();
            return Ok(phones);
        }
    }
}
