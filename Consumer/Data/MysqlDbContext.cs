using Microsoft.EntityFrameworkCore;
using Consumer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Data
{
    public class MysqlDbContext: DbContext
    {
        public DbSet<StationInformation> StationInformation { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public MysqlDbContext(DbContextOptions<MysqlDbContext> options)
        : base(options)
        {
        }

    }
}
