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


        // Construtor vazio para o EF
        public Application() { }

        // Construtor com parâmetros para uso explícito.
        public Application(string nameId, string displayName, string version, string filename, string argument, string source, string filter, string hash)
        {
            NameID = nameId;
            DisplayName = displayName;
            Version = version;
            FileName = filename;
            Argument = argument;
            Source = source;
            Filter = filter;
            Hash = hash;
        }

        // Applications associado ao aplicativo
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

        // DeviceModel associado ao aplicativo
        public virtual ICollection<DeviceModel> DeviceModels { get; set; } = new List<DeviceModel>();

        // Dispositivo associado ao aplicativo
        public virtual ICollection<ProfileDeploy> ProfileDeploys { get; set; } = new List<ProfileDeploy>();
    }
}
