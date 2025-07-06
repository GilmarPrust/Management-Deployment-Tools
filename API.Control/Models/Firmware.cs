
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

        // Construtor com parâmetros para uso explícito  
        public Firmware(string filename, string version, string source, string hash, Guid deviceModelId, DeviceModel deviceModel)
        {
            FileName = filename;
            Version = version;
            Source = source;
            Hash = hash;
            DeviceModelId = deviceModelId;
            DeviceModel = deviceModel;
        }

        // Modelo de Dispositivo associado ao firmware.  
        public required Guid DeviceModelId { get; set; }
        public required virtual DeviceModel DeviceModel { get; set; }
    }
}
