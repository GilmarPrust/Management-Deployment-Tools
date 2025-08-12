using DCM.Application.Mappings;

namespace DCM.API.Helpers
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
                cfg.AddProfile(new AppxPackageProfile());
                cfg.AddProfile(new DeployProfileProfile());
                cfg.AddProfile(new DeviceModelProfile());
                cfg.AddProfile(new DeviceProfile());
                cfg.AddProfile(new DriverPackProfile());
                cfg.AddProfile(new FirmwareProfile());
                cfg.AddProfile(new ImageProfile());
                cfg.AddProfile(new InventoryProfile());
                cfg.AddProfile(new ManufacturerProfile());
                cfg.AddProfile(new OperatingSystemProfile());
                cfg.AddProfile(new ProfileTaskProfile());
            });
            return services;
        }
    }
}
