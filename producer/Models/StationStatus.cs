using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace producer.Models;

public class StationStatus
{
    [JsonPropertyName("station_id")]
    public string StationId { get; set; } = string.Empty;

    [JsonPropertyName("num_bikes_available")]
    [Range(0, int.MaxValue)]
    public int NumBikesAvailable { get; set; }

    [JsonPropertyName("num_docks_available")]
    [Range(0, int.MaxValue)]
    public int NumDocksAvailable { get; set; }

    [JsonPropertyName("is_renting")]
    [Range(0, 1)]
    public int IsRenting { get; set; }

    [JsonPropertyName("is_returning")]
    [Range(0, 1)]
    public int IsReturning { get; set; }

    [JsonPropertyName("last_reported")]
    public long LastReported { get; set; }
}