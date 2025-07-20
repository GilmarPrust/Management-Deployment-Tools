using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um pacote de driver associado a um modelo de dispositivo.
    /// </summary>
    public class DriverPackOEM : DriverPack
    {
        [Required]
        public required Guid DeviceModelId { get; init; }
        public virtual DeviceModel DeviceModel { get; init; } = null!;
    }
}
