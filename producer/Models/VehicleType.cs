using System.Text.Json.Serialization;

namespace producer.Models;

public class VehicleType
{
    [JsonPropertyName("vehicle_type_id")]
    public string VehicleTypeId { get; set; } = string.Empty;

    [JsonPropertyName("form_factor")]
    public string FormFactor { get; set; } = string.Empty;

    [JsonPropertyName("propulsion_type")]
    public string PropulsionType { get; set; } = string.Empty;

    [JsonPropertyName("max_range_meters")]
    public double? MaxRangeMeters { get; set; }
}