using ColdStart.Api.Models.Enums.Vehicle;

namespace ColdStart.Api.Models.DTOs;

public class CreateModelRequest
{
    public required string Name { get; set; } = string.Empty;
    public required int BrandId { get; set; }
    public required BodyStyle BodyStyle { get; set; }
    public required SizeSegment Size { get; set; }
    public required FuelType FuelType { get; set; }
}
