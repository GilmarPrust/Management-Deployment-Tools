using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.DeviceModel
{
    public class DeviceModelAddApplicationDTO
    {
        [Required]
        public List<Guid> ApplicationIds { get; set; } = new();
    }
}