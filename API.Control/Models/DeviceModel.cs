using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DeviceModel
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public string Type { get; set; } = string.Empty;

        // Firmware associado ao modelo de dispositivo.
        public Guid? FirmwareId { get; set; }
        public virtual Firmware? Firmware { get; set; }


        // DriverPacks associado ao modelo de dispositivo.
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();


        // Dispositivos associados ao modelo de dispositivo.
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();


        // Aplicativos associados ao modelo de dispositivo.
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    }
}
