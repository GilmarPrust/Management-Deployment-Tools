using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs
{
    public class DeviceModelDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Manufacturer { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
