using Consumer.Models;
using Consumer.Services;

namespace Consumer.Handlers;

public class StationInformationHandler
{
    private readonly StationInformationService _service;

    public StationInformationHandler(
        StationInformationService service)
    {
        _service = service;
    }

    public async Task HandleAsync(
        StationInformation station,
        CancellationToken cancellationToken = default)
    {
        await _service.AddOrUpdateAsync(station);
    }
}