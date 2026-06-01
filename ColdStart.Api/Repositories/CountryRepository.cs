using ColdStart.Api.Data;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;

namespace ColdStart.Api.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(AppDbContext context)
        : base(context) { }
}
