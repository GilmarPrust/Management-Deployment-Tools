using System.ComponentModel.DataAnnotations;

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

        // Construtor com parâmetros para uso explícito.
        public DeviceModel(string manufacturer, string model, string type)
        {
            Manufacturer = manufacturer;
            Model = model;
            Type = type;
        }


        // Firmware associado ao modelo de dispositivo.
        public Guid FirmwareId { get; set; } = Guid.Empty;
        public virtual Firmware Firmware { get; set; } = null!;

        // DriverPacks associado ao modelo de dispositivo.
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();

        // Aplicativos associados ao modelo de dispositivo.
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    }
}
