using producer.DTOs;
using producer.Models;
using System.Text.Json;

namespace producer.Services;

public class VehicleTypeService
{
    private readonly HttpClient _client;
    private readonly string _vehicleTypeUrl =  "https://gbfs.lyft.com/gbfs/2.3/bkn/en/vehicle_types.json";
    public VehicleTypeService(HttpClient client)
    {
        _client = client;
    }

    private async Task<HttpResponseMessage> SendRequestAsync()
    {
        HttpResponseMessage response = await _client.GetAsync(_vehicleTypeUrl);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<IEnumerable<VehicleType>> GetVehicleTypesAsync()
    {
        VehicleTypeResponseDto? vehicleResponse = null;

        HttpResponseMessage response = await SendRequestAsync();

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