using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.AppxPackage
{
    public class AppxPackageUpdateDTO
    {
        [Required]
        public string ComputerName { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public string MacAddress { get; set; } = string.Empty;

        [Required]
        public Guid DeviceModelId { get; set; }

        [Required]
        public bool Enabled { get; init; }
    }
}
