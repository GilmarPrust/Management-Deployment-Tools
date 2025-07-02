using API.Control.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Control.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Device> Device { get; set; }

        public DbSet<DeviceModel> DeviceModel { get; set; }

        /*public DbSet<Application> Application { get; set; }

        public DbSet<DeviceApplication> DeviceApplication { get; set; }

        public DbSet<DeviceIDOffice> DeviceIDOffice { get; set; }

        public DbSet<DeviceModelApplication> DeviceModelApplication { get; set; }

        public DbSet<DeviceProfile> DeviceProfile { get; set; }

        public DbSet<DriverPack> DriverPack { get; set; }

        public DbSet<Firmware> Firmware { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<Profile> Profile { get; set; }*/


    }
}
