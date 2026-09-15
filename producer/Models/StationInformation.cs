using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public float Lat {  get; set; }
    [JsonPropertyName("lon")]
    public float Lon {  get; set; }
    [JsonPropertyName("capacity")]
    public int Capacity {  get; set; }
}

