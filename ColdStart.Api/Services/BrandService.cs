using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;
using ColdStart.Api.Services.Interfaces;

namespace ColdStart.Api.Services;

public class BrandService : Service<Brand>, IBrandService
{
    public BrandService(IBrandRepository brandRepository)
        : base(brandRepository) { }
}
