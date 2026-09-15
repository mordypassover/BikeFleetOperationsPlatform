using System.Text.Json.Serialization;

namespace producer.DTOs;

public class VehicleTypeResponseDto
{
    [JsonPropertyName("data")]
    public VehicleTypeDataDto? Data { get; set; }

    [JsonPropertyName("last_updated")]
    public long LastUpdated { get; set; }

    [JsonPropertyName("ttl")]
    public int Ttl { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
}