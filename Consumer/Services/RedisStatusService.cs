using System.Text.Json;
using Consumer.Models;
using StackExchange.Redis;

namespace Consumer.Services;

public class RedisStatusService
{
    private readonly IConnectionMultiplexer _redis;

    public RedisStatusService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }


    public async Task<bool> StationExistsAsync(string stationId)
    {
        var database = _redis.GetDatabase();

        var key = $"station:{stationId}";

        return await database.KeyExistsAsync(key);
    }


    public async Task AddStationAsync(string stationId)
    {
        var database = _redis.GetDatabase();

        var key = $"station:{stationId}";

        await database.StringSetAsync(key, "exists");
    }

    public async Task<StationStatus?> GetAsync(string stationId)
    {
        var database = _redis.GetDatabase();

        var key = $"station-status:{stationId}";

        var value = await database.StringGetAsync(key);

        if (value.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<StationStatus>(
            value.ToString());
    }


    public async Task SetAsync(StationStatus status)
    {
        var database = _redis.GetDatabase();

        var key = $"station-status:{status.StationId}";

        var json = JsonSerializer.Serialize(status);

        await database.StringSetAsync(key, json);
    }
}