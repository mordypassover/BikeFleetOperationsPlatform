using producer.DTOs;
using producer.Models;
using System.Text.Json;

namespace producer.Services;

public class StationStatusService
{
    private readonly HttpClient _client;
    private readonly string _stationStatusUrl = "https://gbfs.lyft.com/gbfs/2.3/bkn/en/station_status.json";

    public StationStatusService(HttpClient client)
    {
        _client = client;
    }

    private async Task<HttpResponseMessage> SendRequestAsync()
    {
        HttpResponseMessage response = await _client.GetAsync(_stationStatusUrl);
        response.EnsureSuccessStatusCode();
        return response;
    }

    public async Task<IEnumerable<StationStatus>> GetStationStatusAsync()
    {
        StationStatusResponseDto? stationResponse = null;

        HttpResponseMessage response = await SendRequestAsync();

        try
        {
            string json = await response.Content.ReadAsStringAsync();

            stationResponse =
                JsonSerializer.Deserialize<StationStatusResponseDto>(json);

            if (stationResponse == null || stationResponse.Data == null)
            {
                throw new InvalidOperationException(
                    "Failed to deserialize the response or data is null.");
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Deserialization failed: {ex.Message}");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
        }

        return stationResponse == null
            ? Enumerable.Empty<StationStatus>()
            : stationResponse.Data.Stations;
    }
}