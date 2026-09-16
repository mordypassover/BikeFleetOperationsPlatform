using Consumer.Models;
using Consumer.Services;

namespace Consumer.Handlers;

public class VehicleTypesHandler
{
    private readonly VehicleTypesService _service;

    public VehicleTypesHandler(
        VehicleTypesService service)
    {
        _service = service;
    }

    public async Task HandleAsync(
        VehicleType vehicleType)
    {
        await _service.AddOrUpdateAsync(vehicleType);
    }
}