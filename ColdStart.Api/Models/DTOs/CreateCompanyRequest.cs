namespace ColdStart.Api.Models.DTOs;

public class CreateCompanyRequest
{
    public required string Name { get; set; } = string.Empty;
    public required int CountryId { get; set; }
    public required bool UserCreated { get; set; }
}
