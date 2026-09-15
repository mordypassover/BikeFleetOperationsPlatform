using System.Text.Json.Serialization;

namespace producer.DTOs;

public class StationStatusResponseDto
{
    [JsonPropertyName("data")]
    public StationStatusDataDto? Data { get; set; }
}