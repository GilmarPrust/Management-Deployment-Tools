using DCM.Application.Services.Implementations;
using DCM.Application.Services.Interfaces;
using DCM.Infrastructure.Persistence;

namespace DCM.API.Helpers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistrations(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IAppxPackageService, AppxPackageService>();
            services.AddScoped<IDeployProfileService, DeployProfileService>();
            services.AddScoped<IDeviceModelService, DeviceModelService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDriverPackService, DriverPackService>();
            services.AddScoped<IFirmwareService, FirmwareService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IProfileTaskService, ProfileTaskService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IOperatingSystemService, OperatingSystemService>();
            return services;
        }
    }
}
