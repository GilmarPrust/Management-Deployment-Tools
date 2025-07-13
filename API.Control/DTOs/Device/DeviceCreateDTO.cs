using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.Device
{
    public class DeviceCreateDTO
    {
        [Required] 
        public string ComputerName { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;

        [Required] 
        public string MacAddress { get; set; } = string.Empty;

        [Required] 
        public Guid DeviceModelId { get; set; }
    }
}
