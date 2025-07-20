using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.Firmware
{
    public class FirmwareUpdateDTO
    {
        [Required]
        public bool Enabled { get; set; }
        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
