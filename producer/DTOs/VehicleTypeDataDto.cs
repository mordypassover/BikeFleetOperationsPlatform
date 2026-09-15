using System.Text.Json.Serialization;
using producer.Models;

namespace producer.DTOs;

public class VehicleTypeDataDto
{
    [JsonPropertyName("vehicle_types")]
    public List<VehicleType> VehicleTypes { get; set; } = new();
}