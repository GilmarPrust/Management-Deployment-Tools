using Microsoft.EntityFrameworkCore;

namespace API.Control.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Adicione DbSet para suas entidades aqui, por exemplo:
        public DbSet<DeviceModel> DeviceModels { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Firmware> Firmwares { get; set; }
        public DbSet<DriverPack> DriverPacks { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProfileDeploy> ProfileDeploys { get; set; }
        public DbSet<DriverPackage> DriverPackages { get; set; }
        public DbSet<AppxPackage> AppxPackages { get; set; }
        
    }
}
