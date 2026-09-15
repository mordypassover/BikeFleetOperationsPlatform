using producer.Models;
using producer.DTOs;
using System.Text.Json;

namespace producer.Services;


public class StationInformationService
{
    private readonly HttpClient _client;
    private readonly string _stationInfoUrl = "https://gbfs.lyft.com/gbfs/2.3/bkn/en/station_information.json";
    public StationInformationService(HttpClient client)
    {
        _client = client;
    }
    private async Task<HttpResponseMessage> SendRequestAsync()
    {
        HttpResponseMessage response = await _client.GetAsync(_stationInfoUrl);
        response.EnsureSuccessStatusCode();
        return response;
    }
    public async Task<IEnumerable<StationInformation>> GetStationInfoAsync()
    {
        StationResponseDto? stationResponse = null;
        HttpResponseMessage response = await SendRequestAsync();
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