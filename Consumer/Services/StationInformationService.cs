using System.Threading.Tasks;
using Consumer.Data;
using Consumer.Models;

namespace Consumer.Services;

public class StationInformationService
{
    private readonly MysqlDbContext _db;
    public StationInformationService(MysqlDbContext db)
    {
        _db = db;
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