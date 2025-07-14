using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um pacote de driver associado a um modelo de dispositivo.
    /// </summary>
    public class DriverPack
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public required string FileName { get; set; }

        [Required, StringLength(50)]
        public required string OS { get; set; }

        [Required, StringLength(50)]
        public required string Version { get; set; }

        [Required, StringLength(200)]
        public required string Source { get; set; }

        [Required, StringLength(64)]
        public required string Hash { get; set; }

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public DriverPack() { }


        // Dispositivos associados ao pacote de driver.
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

        public Guid DeviceModelId { get; set; }

        public virtual DeviceModel? DeviceModel { get; set; }

    }
}
