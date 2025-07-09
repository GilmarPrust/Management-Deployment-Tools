using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DeviceModel
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Manufacturer { get; init; } = string.Empty;
        public string Model { get; init; } = string.Empty;
        public string Type { get; set; } = string.Empty;


        //Contrutor vazio para o EF
        public DeviceModel() { }

        // Construtor com parâmetros para uso explícito.
        public DeviceModel(string manufacturer, string model, string type)
        {
            if (string.IsNullOrWhiteSpace(manufacturer))
                throw new ArgumentException("Manufacturer is required.", nameof(manufacturer));
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model is required.", nameof(model));

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
