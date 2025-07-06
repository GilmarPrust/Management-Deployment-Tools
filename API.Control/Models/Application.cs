namespace API.Control.Models
{
    public class Application
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string NameID { get; set; }
        public required string DisplayName { get; set; }
        public required string Version { get; set; }
        public required string FileName { get; set; }
        public string Argument { get; set; } = string.Empty;
        public required string Source { get; set; }
        public string Filter { get; set; } = string.Empty;
        public string Hash { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;


        // Dispositivos associados ao aplicativo.
        public virtual ICollection<Device>? Devices { get; set; } = new List<Device>();

        // Modelos de dispositivo associado ao aplicativo.
        public virtual ICollection<DeviceModel>? DeviceModels { get; set; } = new List<DeviceModel>();

        // Profiles associados ao aplicativo.
        public virtual ICollection<ProfileDeploy>? ProfileDeploys { get; set; } = new List<ProfileDeploy>();
    }
}
