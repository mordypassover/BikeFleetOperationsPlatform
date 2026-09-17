using Consumer.Models;
using Consumer.Services;

namespace Consumer.Handlers;

public class StationInformationHandler
{
    private readonly StationInformationService _service;
    private readonly RedisStatusService _redis;

    public StationInformationHandler(
        StationInformationService service,
        RedisStatusService redis)
    {
        _service = service;
        _redis = redis;
    }

    public async Task HandleAsync(
        StationInformation station,
        CancellationToken cancellationToken = default)
    {
        var exists = await _redis.StationExistsAsync(
            station.Station_id);

        if (!exists)
        {
            await _redis.AddStationAsync(
                station.Station_id);
        }

        await _service.AddOrUpdateAsync(station);
    }
}