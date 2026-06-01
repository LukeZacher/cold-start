using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;
using ColdStart.Api.Services.Interfaces;

namespace ColdStart.Api.Services;

public class CompanyService : Service<Company>, ICompanyService
{
    public CompanyService(ICompanyRepository companyRepository)
        : base(companyRepository) { }
}
