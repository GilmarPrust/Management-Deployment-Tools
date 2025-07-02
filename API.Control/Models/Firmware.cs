namespace API.Control.Models
{
    public class Firmware
    {
        public string DeviceModelGuid { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
    }
}
