using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SWD.PhoneStoreManagement.Repository.Entity;
using SWD.PhoneStoreManagement.Repository.Request.Model;
using SWD.PhoneStoreManagement.Service.Interface;

namespace SWD.PhoneStoreManagement.Api.Controllers
{
    [ApiController]
    [Route("odata/[controller]")]
    public class ModelController : ODataController
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllModels()
        {
            var models = await _modelService.GetAllModelsAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetModelById(int id)
        {
            var model = await _modelService.GetModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddModel([FromBody] ModelDto modelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = new Model
            {
                ModelName = modelDto.ModelName + " OperatingSystem: " + modelDto.OperatingSystem + " Ram: " + modelDto.Ram + " Storage: " + modelDto.Storage,
                BrandId = modelDto.BrandId,
                ReleaseDate = DateOnly.FromDateTime(DateTime.Now),
                OperatingSystem = modelDto.OperatingSystem,
                Ram = modelDto.Ram,
                Storage = modelDto.Storage
            };

            await _modelService.AddModelAsync(model);
            return CreatedAtAction(nameof(GetModelById), new { id = model.ModelId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] ModelDto modelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingModel = await _modelService.GetModelByIdAsync(id);
            if (existingModel == null)
            {
                return NotFound();
            }

            // Map DTO fields to the existing entity
            existingModel.BrandId = modelDto.BrandId;
            existingModel.ModelName = modelDto.ModelName;
            existingModel.OperatingSystem = modelDto.OperatingSystem;
            existingModel.Ram = modelDto.Ram;
            existingModel.Storage = modelDto.Storage;

            await _modelService.UpdateModelAsync(existingModel);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            await _modelService.DeleteModelAsync(id);
            return NoContent();
        }
    }

}
