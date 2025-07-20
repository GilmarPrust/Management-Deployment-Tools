using API.Control.DTOs.Application;
using API.Control.DTOs.DriverPack;
using API.Control.DTOs.Firmware;

namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelReadDTO
    {
        public Guid Id { get; init; }
        public string Manufacturer { get; init; } = string.Empty;
        public string Model { get; init; } = string.Empty;
        public string Type { get; init; } = string.Empty;
        public bool Enabled { get; init; }

        public FirmwareReadDTO? Firmware { get; init; } = null;
        public List<DriverPackReadDTO>? DriverPacks { get; init; } = null;
        public List<Guid> ApplicationIds { get; init; } = new();
        public List<Guid> DeviceIds { get; init; } = new();

    }
}
