using API.Control.DTOs.Application;
using API.Control.DTOs.AppxPackage;
using API.Control.DTOs.DriverPackage;
using API.Control2.DTOs;

namespace API.Control.DTOs.DeviceModel
{
    public class DeviceModelReadDTO
    {
        public Guid Id { get; init; }
        public string Manufacturer { get; init; } = string.Empty;
        public string Model { get; init; } = string.Empty;
        public string Type { get; init; } = string.Empty;

        public FirmwareReadDTO? Firmware { get; set; }
        public List<DriverPackReadDTO> DriverPacks { get; init; } = new();
        public List<AppxPackageReadDTO> AppxPackages { get; init; } = new();
        public List<ApplicationReadDTO> Applications { get; init; } = new();
    }
}
