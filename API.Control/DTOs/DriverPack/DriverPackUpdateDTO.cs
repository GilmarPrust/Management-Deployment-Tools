using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.DriverPack
{
    public class DriverPackUpdateDTO
    {
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

        [Required]
        public bool Enabled { get; set; }
    }
}
