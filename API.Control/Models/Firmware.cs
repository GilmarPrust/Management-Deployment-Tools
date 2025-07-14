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
        public required string FileName { get; set; }

        [Required, StringLength(50)]
        public required string Version { get; set; }

        [Required, StringLength(250)]
        public required string Source { get; set; }

        [Required, StringLength(64)]
        public required string Hash { get; set; }

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF  
        public Firmware() { }

        [Required]
        public required Guid DeviceModelId { get; set; }

        [Required]
        public required virtual DeviceModel DeviceModel { get; set; }
    }
}
