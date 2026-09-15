using producer.DTOs;
using producer.Models;
using System.Text.Json;

namespace producer.Services;

public class StationStatusSubtractor
{
    private readonly HttpClient _client;

    public StationStatusSubtractor(HttpClient client)
    {
        _client = client;
    }

    private async Task<HttpResponseMessage> SendRequestAsync(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return response;
    }

    public async Task<IEnumerable<StationStatus>> GetStationStatusAsync(string url)
    {
        StationStatusResponseDto? stationResponse = null;

        HttpResponseMessage response = await SendRequestAsync(url);

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