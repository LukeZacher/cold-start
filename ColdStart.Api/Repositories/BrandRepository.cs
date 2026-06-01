using ColdStart.Api.Data;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;

namespace ColdStart.Api.Repositories;

public class BrandRepository : Repository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context)
        : base(context) { }
}
