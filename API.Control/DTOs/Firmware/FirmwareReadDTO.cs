namespace API.Control.DTOs.Firmware
{
    public class FirmwareReadDTO
    {
        public Guid Id { get; init; }
        public string FileName { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string Hash { get; init; } = string.Empty;
        public bool Enabled { get; init; }
        public Guid DeviceModelId { get; set; }
    }
}
