using ColdStart.Api.Data;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;

namespace ColdStart.Api.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(AppDbContext context)
        : base(context) { }
}
