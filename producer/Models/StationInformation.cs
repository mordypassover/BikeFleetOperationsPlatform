using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace producer.Models;

public class StationInformation
{
    [JsonPropertyName("station_id")]
    public string Station_id {  get; set; }
    [JsonPropertyName("name")]
    public string Name {  get; set; }
    [JsonPropertyName("lat")]
    [Range(-90, 90)]
    public float Lat {  get; set; }

    [JsonPropertyName("lon")]
    [Range(-180, 180)]
    public float Lon {  get; set; }
    [JsonPropertyName("capacity")]
    [Range(0, int.MaxValue)]
    public int Capacity {  get; set; }
}

