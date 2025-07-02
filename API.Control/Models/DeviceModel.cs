using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DeviceModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O Fabricante do dispositivo é obrigatório.")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Modelo do dispositivo é obrigatório.")]
        public string Model { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

    }
}
