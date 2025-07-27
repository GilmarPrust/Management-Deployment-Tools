namespace API.Control.DTOs.Firmware
{
    public class FirmwareUpdateDTO
    {
        [Required]
        public string FileName { get; set; } = string.Empty;
        [Required]
        public string Version { get; set; } = string.Empty;
        [Required]
        public string Source { get; set; } = string.Empty;
        [Required]
        public string Hash { get; set; } = string.Empty;

        public bool Enabled { get; init; }

        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
