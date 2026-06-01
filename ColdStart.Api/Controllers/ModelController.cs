using ColdStart.Api.Models.DTOs;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ColdStart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModelController : ControllerBase
{
    private readonly IModelService _modelService;

    public ModelController(IModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Model> models = await _modelService.GetAllAsync();
        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Model? model = await _modelService.GetByIdAsync(id);
        if (model == null)
            return NotFound();
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModelRequest createModelRequest)
    {
        Model model = new Model
        {
            Name = createModelRequest.Name,
            BrandId = createModelRequest.BrandId,
            BodyStyle = createModelRequest.BodyStyle,
            Size = createModelRequest.Size,
            FuelType = createModelRequest.FuelType,
        };

        Model createdModel = await _modelService.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = createdModel.Id }, createdModel);
    }
}
