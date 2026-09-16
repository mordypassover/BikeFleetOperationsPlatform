using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Consumer.Models;

public class StationInformation
{
    [JsonPropertyName("station_id")]
    [Required]
    [Key]
    public string Station_id {  get; set; }
    [JsonPropertyName("name")]
    [Required]
    public string Name {  get; set; }
    [JsonPropertyName("lat")]
    [Required]
    public float Lat {  get; set; }
    [JsonPropertyName("lon")]
    [Required]
    public float Lon {  get; set; }
    [JsonPropertyName("capacity")]
    [Required]
    public int Capacity {  get; set; }
}

