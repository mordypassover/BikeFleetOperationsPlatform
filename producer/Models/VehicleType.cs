using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


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
    [Range(0, double.MaxValue)]
    public double? MaxRangeMeters { get; set; }
}