namespace ColdStart.Api.Models.Entities;

public class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool UserCreated { get; set; }

    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;

    public ICollection<Brand> Brands { get; set; } = new List<Brand>();
    public ICollection<Engine> Engines { get; set; } = new List<Engine>();
}
