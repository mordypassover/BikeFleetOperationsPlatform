using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Consumer.Models;

public class StationStatus
{
    [JsonPropertyName("station_id")]
    [Key]
    [Required]
    public string StationId { get; set; } = string.Empty;

    [JsonPropertyName("num_bikes_available")]
    public int NumBikesAvailable { get; set; }

    [JsonPropertyName("num_docks_available")]
    public int NumDocksAvailable { get; set; }

    [JsonPropertyName("is_renting")]
    public int IsRenting { get; set; }

    [JsonPropertyName("is_returning")]
    public int IsReturning { get; set; }

    [JsonPropertyName("last_reported")]
    public long LastReported { get; set; }
}