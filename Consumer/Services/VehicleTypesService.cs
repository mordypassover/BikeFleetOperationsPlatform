using Consumer.Data;
using Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Services;

public class VehicleTypesService
{
    private readonly MysqlDbContext _db;

    public VehicleTypesService(MysqlDbContext db)
    {
        _db = db;
    }

    public async Task AddOrUpdateAsync(VehicleType vehicleType)
    {
        var existing = await _db.VehicleTypes.FindAsync(vehicleType.VehicleTypeId);

        if (existing == null)
        {
            await _db.VehicleTypes.AddAsync(vehicleType);
        }
        else
        {
            bool isModified = existing.FormFactor != vehicleType.FormFactor ||
                              existing.PropulsionType != vehicleType.PropulsionType ||
                              existing.MaxRangeMeters != vehicleType.MaxRangeMeters;

            if (!isModified) return;

            existing.FormFactor = vehicleType.FormFactor;
            existing.PropulsionType = vehicleType.PropulsionType;
            existing.MaxRangeMeters = vehicleType.MaxRangeMeters;
        }

        await _db.SaveChangesAsync();
    }
}
