using ColdStart.Api.Models.Enums.Vehicle;

namespace ColdStart.Api.Models.Entities;

public class Vehicle
{
    public int Id { get; set; }

    public int Year { get; set; }
    public string Trim { get; set; } = string.Empty;
    public int NumDoors { get; set; }
    public int PassengerCapacity { get; set; }
    public int Msrp { get; set; }

    // Performance / powertrain
    public Drivetrain Drivetrain { get; set; }
    public TransmissionType Transmission { get; set; }
    public int? MpgCity { get; set; }
    public int? MpgHighway { get; set; }
    public int? ElectricRangeMiles { get; set; }
    public double ZeroToSixty { get; set; }
    public int Reliability { get; set; }

    // Size measurements
    public double LengthInches { get; set; }
    public double WidthInches { get; set; }
    public double HeightInches { get; set; }
    public int CurbWeightLbs { get; set; }

    // Truckish properties
    public int PayloadCapacity { get; set; }
    public int? TowingCapacity { get; set; }
    public double? BedLength { get; set; }
    public double? BedWidth { get; set; }
    public CabType? CabType { get; set; }
    public bool? Dually { get; set; }

    public int EngineId { get; set; }
    public int ModelId { get; set; }
    public Engine Engine { get; set; } = null!;
    public Model Model { get; set; } = null!;
}
