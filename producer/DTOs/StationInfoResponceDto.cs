using System.Text.Json.Serialization;

namespace producer.DTOs;

public class StationResponseDto
{
    [JsonPropertyName("data")]
    public StationInfoDto Data { get; set; }
}