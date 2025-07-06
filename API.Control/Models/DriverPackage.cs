namespace API.Control.Models
{
    public class DriverPackage
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string FileName { get; set; }
        public string OS { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public required string Source { get; set; }
        public string Hash { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        // Dispositivos associados ao pacote de driver.
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
