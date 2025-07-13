using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.Control.Models
{
    public class DeviceModel
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public string Type { get; set; } = string.Empty;


        //Contrutor vazio para o EF
        public DeviceModel() { }


        // Opcional: Firmware pode ou não estar associado
        public virtual Firmware? Firmware { get; set; }

        // Dispositivos associados ao modelo de dispositivo.
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

        // DriverPacks associado ao modelo de dispositivo.
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();

        // Aplicativos associados ao modelo de dispositivo.
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    }
}
