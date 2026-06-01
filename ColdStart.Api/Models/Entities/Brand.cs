using ColdStart.Api.Models.Enums;

namespace ColdStart.Api.Models.Entities;

public class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public BrandType Type { get; set; }
    public string? Description { get; set; } = string.Empty;
    public int FoundedYear { get; set; }
    public bool UserCreated { get; set; }

    // Brand colors (future)

    public int CountryId { get; set; }
    public int CompanyId { get; set; }
    public Country Country { get; set; } = null!;
    public Company Company { get; set; } = null!;

    public ICollection<Model> Models { get; set; } = new List<Model>();
}
