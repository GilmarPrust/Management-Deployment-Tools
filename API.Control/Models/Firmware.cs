using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um firmware associado a um modelo de dispositivo.
    /// </summary>
    public class Firmware
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public required string FileName { get; init; }

        [Required, StringLength(50)]
        public required string Version { get; init; }

        [Required, StringLength(250)]
        public required string Source { get; init; }

        [Required, StringLength(64)]
        public required string Hash { get; init; }

        public bool Enabled { get; set; } = true;

        public Firmware() { }


        [Required]
        public required Guid DeviceModelId { get; init; }

        public virtual DeviceModel DeviceModel { get; init; } = null!;
    }
}
