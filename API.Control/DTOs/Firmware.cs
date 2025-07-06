using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs
{
    public class FirmwareDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string Version { get; set; }

        [Required]
        public string Source { get; set; } = string.Empty;

        public string Hash { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
