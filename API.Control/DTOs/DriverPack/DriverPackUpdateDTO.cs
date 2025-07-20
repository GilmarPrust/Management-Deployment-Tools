using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.DriverPack
{
    public class DriverPackUpdateDTO
    {
        [Required]
        public bool Enabled { get; set; }
        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
