using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Consumer.Models;

public class VehicleType
{
    [JsonPropertyName("vehicle_type_id")]
    [Required]
    [Key]
    public string VehicleTypeId { get; set; } = string.Empty;

    [JsonPropertyName("form_factor")]
    [Required]      
    public string FormFactor { get; set; } = string.Empty;

    [JsonPropertyName("propulsion_type")]
    [Required]
    public string PropulsionType { get; set; } = string.Empty;

    [JsonPropertyName("max_range_meters")]
    public double? MaxRangeMeters { get; set; }
}