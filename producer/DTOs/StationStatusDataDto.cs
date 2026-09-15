using producer.Models;
using System.Text.Json.Serialization;

namespace producer.DTOs;

public class StationStatusDataDto
{
    [JsonPropertyName("stations")]
    public List<StationStatus> Stations { get; set; } = new();
}