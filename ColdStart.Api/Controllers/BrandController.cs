using ColdStart.Api.Models.DTOs;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ColdStart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Brand> brands = await _brandService.GetAllAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Brand? brand = await _brandService.GetByIdAsync(id);
        if (brand == null)
            return NotFound();
        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandRequest createBrandRequest)
    {
        Brand brand = new Brand
        {
            Name = createBrandRequest.Name,
            Type = createBrandRequest.Type,
            FoundedYear = createBrandRequest.FoundedYear,
            CountryId = createBrandRequest.CountryId,
            CompanyId = createBrandRequest.CompanyId,
            UserCreated = createBrandRequest.UserCreated,
        };
        Brand createdBrand = await _brandService.CreateAsync(brand);
        return CreatedAtAction(nameof(GetById), new { id = createdBrand.Id }, createdBrand);
    }
}
