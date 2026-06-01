using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;
using ColdStart.Api.Services.Interfaces;

namespace ColdStart.Api.Services;

public class CountryService : Service<Country>, ICountryService
{
    public CountryService(ICountryRepository countryRepository)
        : base(countryRepository) { }
}
