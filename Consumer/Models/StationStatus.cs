using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;


namespace Consumer.Models;

public class StationStatus
{
    [JsonPropertyName("station_id")]
    [Required]
    [Key]
    public string StationId { get; set; } = string.Empty;

    [JsonPropertyName("num_bikes_available")]
    [Required]
    public int NumBikesAvailable { get; set; }

    [JsonPropertyName("num_docks_available")]
    [Required]  
    public int NumDocksAvailable { get; set; }

    [JsonPropertyName("is_renting")]
    [Required]
    public int IsRenting { get; set; }

    [JsonPropertyName("is_returning")]
    [Required]
    public int IsReturning { get; set; }

    [JsonPropertyName("last_reported")]
    [Required]
    public long LastReported { get; set; }
}