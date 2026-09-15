using producer.Models;
using producer.DTOs;
using System.Text.Json;

namespace producer.Services;


public class StationInfoSubtractor
{
    private readonly HttpClient _client;
    public StationInfoSubtractor(HttpClient client)
    {
        _client = client;
    }
    private async Task<HttpResponseMessage> SendRequestAsync(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return response;
    }
    public async Task<IEnumerable<StationInformation>> GetStationInfoAsync(string url)
    {
        StationResponseDto? stationResponse = null;
        HttpResponseMessage response = await SendRequestAsync(url);
        try
        {
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            stationResponse = JsonSerializer.Deserialize<StationResponseDto>(json);
            if (stationResponse == null || stationResponse.Data == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response or data is null.");
            }
        }
            catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Argument null failed: {ex.Message}");
        }
            catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }
       
        return stationResponse == null ?
            Enumerable.Empty<StationInformation>() :
                stationResponse.Data.Stations;
    }
}