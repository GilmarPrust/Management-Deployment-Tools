using API.Control.DTOs.Application;
using API.Control.DTOs.AppxPackage;
using API.Control.DTOs.DeviceModel;
using API.Control.DTOs.ProfileDeploy;
using API.Control.DTOs.DriverPack;

namespace API.Control.DTOs.Device
{
    /// <summary>
    /// DTO para leitura de dispositivo.
    /// </summary>
    public class DeviceReadDTO
    {
        public Guid Id { get; init; }
        public string ComputerName { get; init; } = string.Empty;
        public string SerialNumber { get; init; } = string.Empty;
        public string MacAddress { get; init; } = string.Empty;
        public Guid DeviceModelId { get; init; }
        public bool Enabled { get; init; }

        public DeviceModelReadDTO DeviceModel { get; init; } = new();
        public ProfileDeployReadDTO? ProfileDeploy { get; init; } = new ProfileDeployReadDTO();
        public List<ApplicationReadDTO> Applications { get; init; } = new();
        public List<AppxPackageReadDTO> AppxPackages { get; init; } = new();
        public List<DriverPackReadDTO> DriverPackages { get; init; } = new();
    }
}
