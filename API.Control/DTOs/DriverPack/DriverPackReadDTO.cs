using API.Control.DTOs.DeviceModel;

namespace API.Control2.DTOs
{
    public class DriverPackReadDTO
    {
        public Guid Id { get; init; }
        public string FileName { get; init; } = string.Empty;
        public string OS { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string Hash { get; init; } = string.Empty;
        public bool Enabled { get; init; }
        public Guid DeviceModelId { get; init; } = Guid.Empty;
        public DeviceModelReadDTO DeviceModel { get; set; } = new DeviceModelReadDTO();

    }
}
