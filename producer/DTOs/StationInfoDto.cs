using  producer.Models;
using System.Text.Json.Serialization;

namespace producer.DTOs;

public class StationInfoDto
{
    [JsonPropertyName("stations")]
    public List<StationInformation> Stations { get; set; }
}