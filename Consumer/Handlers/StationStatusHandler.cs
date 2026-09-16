using Consumer.Models;
using Consumer.Services;

namespace Consumer.Handlers;

public class StationStatusHandler
{
    private readonly RedisStatusService _redis;
    private readonly MongoStatusService _mongo;

    public StationStatusHandler(
        RedisStatusService redis,
        MongoStatusService mongo)
    {
        _redis = redis;
        _mongo = mongo;
    }

    public async Task HandleAsync(StationStatus status)
    {
        var previousStatus =
            await _redis.GetAsync(status.StationId);

        if (previousStatus == null)
        {
            await _mongo.AddAsync(status);

            await _redis.SetAsync(status);

            return;
        }

        if (IsSameStatus(previousStatus, status))//no changes
        {
            return;
        }


        await _mongo.AddAsync(status);

        await _redis.SetAsync(status);
    }

    private static bool IsSameStatus(
        StationStatus oldStatus,
        StationStatus newStatus)
    {
        return
            oldStatus.NumBikesAvailable ==
                newStatus.NumBikesAvailable &&

            oldStatus.NumDocksAvailable ==
                newStatus.NumDocksAvailable &&

            oldStatus.IsRenting ==
                newStatus.IsRenting &&

            oldStatus.IsReturning ==
                newStatus.IsReturning;
    }
}