using System.ComponentModel.DataAnnotations;

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

        [Required]
        public bool Enabled { get; set; }

        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
