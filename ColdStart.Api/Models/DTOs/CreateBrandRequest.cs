using ColdStart.Api.Models.Enums;

namespace ColdStart.Api.Models.DTOs;

public class CreateBrandRequest
{
    public required string Name { get; set; }
    public required BrandType Type { get; set; }
    public required int FoundedYear { get; set; }
    public required int CountryId { get; set; }
    public required int CompanyId { get; set; }
    public required bool UserCreated { get; set; }
}
