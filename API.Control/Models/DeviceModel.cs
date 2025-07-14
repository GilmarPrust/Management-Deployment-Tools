using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um modelo de dispositivo, incluindo fabricante, modelo, tipo e associações.
    /// </summary>
    public class DeviceModel
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public required string Manufacturer { get; set; }

        [Required, StringLength(100)]
        public required string Model { get; set; }

        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public DeviceModel() { }

        public virtual Firmware? Firmware { get; set; }
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
