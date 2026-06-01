namespace ColdStart.Api.Models.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PrimaryColor { get; set; } = string.Empty;
    public string SecondaryColor { get; set; } = string.Empty;

    public ICollection<Brand> Brands { get; set; } = new List<Brand>();
    public ICollection<Company> Companies { get; set; } = new List<Company>();
}
