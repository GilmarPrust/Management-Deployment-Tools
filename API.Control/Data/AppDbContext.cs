using API.Control.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace API.Control.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Adicione DbSet para suas entidades aqui, por exemplo:
        public DbSet<Application> Applications { get; set; }
        public DbSet<AppxPackage> AppxPackages { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceModel> DeviceModels { get; set; }
        public DbSet<DriverPack> DriverPacks { get; set; }
        public DbSet<Firmware> Firmwares { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryInfo> InventoryInfos { get; set; }
        public DbSet<DeployProfile> ProfileDeploys { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // DEVICE
            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.SerialNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(d => d.MacAddress)
                    .HasConversion(
                        mac => mac.Value,
                        value => MacAddress.Create(value)
                    );

                entity.HasOne(d => d.DeviceModel)
                      .WithMany(dm => dm.Devices)
                      .HasForeignKey(d => d.DeviceModelId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.OwnsOne(d => d.ComputerName);

            });

            // DEVICE MODEL
            modelBuilder.Entity<DeviceModel>(entity =>
            {
                entity.HasKey(dm => dm.Id);

                entity.Property(dm => dm.Manufacturer)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(dm => dm.Model)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(dm => dm.Type)
                      .HasMaxLength(50);

                // Relacionamento 1:1 com Firmware
                entity.HasOne(dm => dm.Firmware)
                      .WithOne(fw => fw.DeviceModel)
                      .HasForeignKey<Firmware>(fw => fw.DeviceModelId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relacionamento 1:N com DriverPack
                entity.HasMany(dm => dm.DriverPacksOEM)
                      .WithOne(dp => dp.DeviceModel)
                      .HasForeignKey(dp => dp.DeviceModelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // FIRMWARE
            modelBuilder.Entity<Firmware>(entity =>
            {
                entity.HasKey(fw => fw.Id);

                entity.Property(fw => fw.FileName).IsRequired().HasMaxLength(100);
                entity.Property(fw => fw.Version).IsRequired().HasMaxLength(50);
                entity.Property(fw => fw.Source).IsRequired().HasMaxLength(200);
                entity.Property(fw => fw.Hash).IsRequired().HasMaxLength(64);
            });

            // DRIVERPACK
            modelBuilder.Entity<DriverPack>(entity =>
            {
                entity.HasKey(dp => dp.Id);

                entity.Property(dp => dp.FileName).IsRequired().HasMaxLength(100);
                entity.Property(dp => dp.OS).IsRequired().HasMaxLength(50);
                entity.Property(dp => dp.Version).IsRequired().HasMaxLength(50);
                entity.Property(dp => dp.Source).IsRequired().HasMaxLength(200);
                entity.Property(dp => dp.Hash).IsRequired().HasMaxLength(64);
            });
        }
    }
}
