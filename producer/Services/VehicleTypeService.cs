using producer.DTOs;
using producer.Models;
using System.Text.Json;

namespace producer.Services;

public class VehicleTypeSubtractor
{
    private readonly HttpClient _client;

    public VehicleTypeSubtractor(HttpClient client)
    {
        _client = client;
    }

    private async Task<HttpResponseMessage> SendRequestAsync(string url)
    {
        HttpResponseMessage response = await _client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<IEnumerable<VehicleType>> GetVehicleTypesAsync(string url)
    {
        VehicleTypeResponseDto? vehicleResponse = null;

        HttpResponseMessage response = await SendRequestAsync(url);

        try
        {
            string json = await response.Content.ReadAsStringAsync();

            vehicleResponse =
                JsonSerializer.Deserialize<VehicleTypeResponseDto>(json);

            if (vehicleResponse == null || vehicleResponse.Data == null)
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

        return vehicleResponse == null
            ? Enumerable.Empty<VehicleType>()
            : vehicleResponse.Data.VehicleTypes;
    }
}