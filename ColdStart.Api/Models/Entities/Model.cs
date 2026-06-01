using ColdStart.Api.Models.Enums.Vehicle;

namespace ColdStart.Api.Models.Entities;

public class Model
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public BodyStyle BodyStyle { get; set; }
    public SizeSegment Size { get; set; }
    public FuelType FuelType { get; set; }
    public int? BaseMsrpNew { get; set; }
    public int? MaxMsrpNew { get; set; }
    public int? WarrantyMiles { get; set; }
    public int? SafetyRating { get; set; }

    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
