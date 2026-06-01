using ColdStart.Api.Models.DTOs;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ColdStart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Company> companies = await _companyService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Company? company = await _companyService.GetByIdAsync(id);
        if (company == null)
            return NotFound();
        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest createCompanyRequest)
    {
        Company company = new Company
        {
            Name = createCompanyRequest.Name,
            CountryId = createCompanyRequest.CountryId,
            UserCreated = createCompanyRequest.UserCreated,
        };

        Company createdCompany = await _companyService.CreateAsync(company);
        return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, createdCompany);
    }
}
