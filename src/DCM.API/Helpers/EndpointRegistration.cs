namespace DCM.API.Helpers
{
    /// <summary>
    /// Responsável por registrar todos os grupos de endpoints da aplicação via descoberta automática.
    /// </summary>
    public static class EndpointRegistration
    {
        /// <summary>
        /// Descobre e registra todos os endpoints implementados via IEndpointDefinition.
        /// </summary>
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            var endpointTypes = typeof(EndpointRegistration).Assembly
                .GetTypes()
                .Where(t => typeof(IEndpointDefinition).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in endpointTypes)
            {
                var instance = (IEndpointDefinition)Activator.CreateInstance(type)!;
                instance.RegisterEndpoints(app);
            }
        }
    }

    /// <summary>
    /// Contrato para definição de grupos de endpoints.
    /// </summary>
    public interface IEndpointDefinition
    {
        void RegisterEndpoints(IEndpointRouteBuilder app);
    }

    /// <summary>
    /// Grupo de endpoints para Application.
    /// </summary>
    public class ApplicationEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/applications")
                .WithTags("Application")
                .WithName("ApplicationEndpoints")
                .WithSummary("Endpoints for managing applications")
                .WithDescription("Provides endpoints to create, read, update, and delete applications.")
                .MapApplicationEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para AppxPackage.
    /// </summary>
    public class AppxPackageEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/appxpackages")
                .WithTags("AppxPackage")
                .WithName("AppxPackageEndpoints")
                .WithSummary("Endpoints for managing AppxPackages")
                .WithDescription("Provides endpoints to create, read, update, and delete AppxPackages.")
                .MapAppxPackageEndPoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para DeployProfile.
    /// </summary>
    public class DeployProfileEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/deployprofiles")
                .WithTags("DeployProfile")
                .WithName("DeployProfileEndpoints")
                .WithSummary("Endpoints for managing DeployProfiles")
                .WithDescription("Provides endpoints to create, read, update, and delete DeployProfiles.")
                .MapDeployProfileEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para Device.
    /// </summary>
    public class DeviceEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/devices")
                .WithTags("Device")
                .WithName("DeviceEndpoints")
                .WithSummary("Endpoints for managing devices")
                .WithDescription("Provides endpoints to create, read, update, and delete devices.")
                .MapDeviceEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para DeviceModel.
    /// </summary>
    public class DeviceModelEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/devicemodels")
                .WithTags("DeviceModel")
                .WithName("DeviceModelEndpoints")
                .WithSummary("Endpoints for managing device models")
                .WithDescription("Provides endpoints to create, read, update, and delete device models.")
                .MapDeviceModelsEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para DriverPack.
    /// </summary>
    public class DriverPackEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/driverpacks")
                .WithTags("DriverPack")
                .WithName("DriverPackEndpoints")
                .WithSummary("Endpoints for managing driver packs")
                .WithDescription("Provides endpoints to create, read, update, and delete driver packs.")
                .MapDriverPackEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para Firmware.
    /// </summary>
    public class FirmwareEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/firmwares")
                .WithTags("Firmware")
                .WithName("FirmwareEndpoints")
                .WithSummary("Endpoints for managing firmwares")
                .WithDescription("Provides endpoints to create, read, update, and delete firmwares.")
                .MapFirmwareEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para Image.
    /// </summary>
    public class ImageEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/images")
                .WithTags("Image")
                .WithName("ImageEndpoints")
                .WithSummary("Endpoints for managing images")
                .WithDescription("Provides endpoints to create, read, update, and delete images.")
                .MapImageEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para Inventory.
    /// </summary>
    public class InventoryEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/inventories")
                .WithTags("Inventory")
                .WithName("InventoryEndpoints")
                .WithSummary("Endpoints for managing inventories")
                .WithDescription("Provides endpoints to create, read, update, and delete inventories.")
                .MapInventoryEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para ProfileTask.
    /// </summary>
    public class ProfileTaskEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/profiletasks")
                .WithTags("ProfileTask")
                .WithName("ProfileTaskEndpoints")
                .WithSummary("Endpoints for managing profile tasks")
                .WithDescription("Provides endpoints to create, read, update, and delete profile tasks.")
                .MapProfileTaskEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para Manufacturer.
    /// </summary>
    public class ManufacturerEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/manufacturers")
                .WithTags("Manufacturer")
                .WithName("ManufacturerEndpoints")
                .WithSummary("Endpoints for managing manufacturers")
                .WithDescription("Provides endpoints to create, read, update, and delete manufacturers.")
                .MapManufacturerEndpoints();
        }
    }

    /// <summary>
    /// Grupo de endpoints para OperatingSystem.
    /// </summary>
    public class OperatingSystemEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGroup("/api/operatingsystems")
                .WithTags("OperatingSystem")
                .WithName("OperatingSystemEndpoints")
                .WithSummary("Endpoints for managing operating systems")
                .WithDescription("Provides endpoints to create, read, update, and delete operating systems.")
                .MapOperatingSystemEndpoints();
        }
    }
}
