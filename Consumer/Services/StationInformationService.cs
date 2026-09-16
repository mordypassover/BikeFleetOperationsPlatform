using System.Threading.Tasks;
using Consumer.Data;
using Consumer.Models;

namespace Consumer.Services;

public class StationInformationService
{
    private readonly MysqlDbContext _db;
    private readonly RedisStatusService _redis;
    public StationInformationService(MysqlDbContext db, RedisStatusService redis)
    {
        _db = db;
        _redis = redis;
    }

    public async Task AddOrUpdateAsync(StationInformation station)
    {
        var existing = await _db.StationInformation.FindAsync(station.Station_id);

        if (existing == null)
        {
            await _db.StationInformation.AddAsync(station);
        }
        else
        {
            bool isModified = existing.Name != station.Name ||
                              existing.Lat != station.Lat ||
                              existing.Lon != station.Lon ||
                              existing.Capacity != station.Capacity;

            if (!isModified) return;

            existing.Name = station.Name;
            existing.Lat = station.Lat;
            existing.Lon = station.Lon;
            existing.Capacity = station.Capacity;
        }

        await _db.SaveChangesAsync();
    }
}