using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.DriverPackage
{
    public class DriverPackageCreateDTO
    {
        [Required] 
        public string FileName { get; set; } = string.Empty;

        public string OS { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        [Required] 
        public string Source { get; set; } = string.Empty;

        [Required]
        public string Hash { get; set; } = string.Empty;


        public List<Guid> DeviceIds { get; set; } = new();
    }
}
