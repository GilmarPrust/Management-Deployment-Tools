using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.DriverPackOEM
{
    public class DriverPackOEMUpdateDTO
    {
        [Required]
        public bool Enabled { get; set; }
        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
