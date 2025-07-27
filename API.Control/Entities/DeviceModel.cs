namespace API.Control.Entities
{
    /// <summary>
    /// Representa um modelo de dispositivo, incluindo fabricante, modelo, tipo e associações.
    /// </summary>
    public class DeviceModel : _BaseEntity
    {

        [Required, StringLength(50)]
        public required string Manufacturer { get; init; }

        [Required, StringLength(50)]
        public required string Model { get; set; }

        [StringLength(50)]
        public string Type { get; set; } = string.Empty;


        // Construtor vazio para o EF
        public DeviceModel() { }


        public virtual Firmware? Firmware { get; set; } = null;
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
        public virtual ICollection<DriverPackOEM> DriverPacksOEM { get; set; } = new List<DriverPackOEM>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
