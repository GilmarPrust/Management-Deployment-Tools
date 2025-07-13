namespace API.Control.Models
{
    public class AppxPackage
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; set; } 
        public required string Version { get; set; }
        public required string Publisher { get; set; }
        public string Architecture { get; set; } = string.Empty;
        public string PackageFamilyName { get; set; } = string.Empty;
        public required string PackageFullName { get; set; }
        public bool IsFramework { get; set; } = false;
        public bool IsBundle { get; set; } = false;
        public bool IsResourcePackage { get; set; } = false;
        public bool IsDevelopmentMode { get; set; } = false;
        public bool IsPartiallyStaged { get; set; } = false;
        public string Status { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public AppxPackage() { }


        // Devices associados ao AppxPackage.  
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
