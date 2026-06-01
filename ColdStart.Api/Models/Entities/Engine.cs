using ColdStart.Api.Models.Enums.Vehicle;

namespace ColdStart.Api.Models.Entities;

public class Engine
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public FuelType FuelType { get; set; }
    public double CylinderSizeLiters { get; set; }
    public int NumCylinders { get; set; }
    public bool Turbocharged { get; set; }
    public bool Supercharged { get; set; }
    public int Horsepower { get; set; }
    public int Torque { get; set; }
    public int YearCreated { get; set; }
    public int Reliability { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
