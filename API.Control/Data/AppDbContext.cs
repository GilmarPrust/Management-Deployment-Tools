namespace API.Control.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets para entidades principais
        public DbSet<Application> Applications { get; set; }
        public DbSet<AppxPackage> AppxPackages { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceModel> DeviceModels { get; set; }
        public DbSet<DriverPack> DriverPacks { get; set; }
        public DbSet<Firmware> Firmwares { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<DeployProfile> DeployProfiles { get; set; }
        public DbSet<ProfileTask> ProfileTasks { get; set; }
        public DbSet<PathToCopy> PathsToCopy { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

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
                        value => new MacAddress(value)
                    );

                entity.Property(d => d.ComputerName)
                    .HasConversion(
                        v => v.Value,
                        value => new ComputerName(value)
                    )
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("ComputerName");

                entity.HasOne(d => d.DeviceModel)
                      .WithMany(dm => dm.Devices)
                      .HasForeignKey(d => d.DeviceModelId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relacionamentos muitos-para-muitos
                entity.HasMany(d => d.Applications)
                      .WithMany(a => a.Devices);

                entity.HasMany(d => d.DriverPacks)
                      .WithMany();

                entity.HasMany(d => d.AppxPackages)
                      .WithMany(a => a.Devices);
            });

            // DEVICE MODEL
            modelBuilder.Entity<DeviceModel>(entity =>
            {
                entity.HasKey(dm => dm.Id);

                // Corrigido: Remover mapeamento direto da propriedade de navegação Manufacturer
                entity.Property(dm => dm.Model)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(dm => dm.Type)
                      .HasMaxLength(50);

                entity.Property(dm => dm.Enabled)
                      .IsRequired();

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

                // Relacionamento muitos-para-muitos com Application
                entity.HasMany(dm => dm.Applications)
                      .WithMany(a => a.DeviceModels);
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

            // DEPLOY PROFILE
            modelBuilder.Entity<DeployProfile>(entity =>
            {
                entity.HasKey(dp => dp.Id);

                entity.Property(dp => dp.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(dp => dp.Description)
                      .HasMaxLength(250);

                entity.HasOne(dp => dp.Image)
                      .WithMany(i => i.DeployProfiles)
                      .HasForeignKey(dp => dp.ImageId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(dp => dp.Applications);
                entity.HasMany(dp => dp.Devices);
                entity.HasMany(dp => dp.ProfileTasks);
            });

            // PATH TO COPY
            modelBuilder.Entity<PathToCopy>(entity =>
            {
                entity.HasKey(pc => pc.Id);

                entity.Property(pc => pc.Path)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // PROFILE TASK
            modelBuilder.Entity<ProfileTask>(entity =>
            {
                entity.HasKey(pt => pt.Id);

                entity.Property(pt => pt.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            // IMAGE
            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.ImageName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(i => i.ImageDescription)
                      .HasMaxLength(250);

                entity.Property(i => i.ImageIndex)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(i => i.ShortName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(i => i.EditionId)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(i => i.Version)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(i => i.Source)
                      .IsRequired()
                      .HasMaxLength(250);

                // Relacionamento com DeployProfiles
                entity.HasMany(i => i.DeployProfiles)
                      .WithOne(dp => dp.Image)
                      .HasForeignKey(dp => dp.ImageId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // INVENTORY
            modelBuilder.Entity<Inventory>()
                .Property(e => e.Hardware)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => string.IsNullOrEmpty(v) ? new Dictionary<string, string>() : JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null)
                );
        }
    }
}
