
namespace API.Control.Models
{
    public class Firmware
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string FileName { get; set; }
        public required string Version { get; set; }
        public required string Source { get; set; }
        public required string Hash { get; set; }
        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF  
        public Firmware() { }


        // Dispositivo associado ao firmware.  
        public required Guid DeviceModelId { get; set; }
        public required virtual DeviceModel DeviceModel { get; set; }
    }
}
