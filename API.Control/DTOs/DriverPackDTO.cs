using System.ComponentModel.DataAnnotations;

namespace API.Control2.DTOs
{
    public class DriverPackDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string OS { get; set; } = string.Empty;

        [Required]
        public string Version { get; set; } = string.Empty;

        [Required]
        public string Source { get; set; } = string.Empty;

        [Required]
        public string Hash { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
