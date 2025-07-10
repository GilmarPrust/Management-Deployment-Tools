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


        // Construtor vazio para o EF
        public AppxPackage() { }

        // Construtor com parâmetros para uso explícito.
        public AppxPackage(string name, string version, string publisher, string architecture, string packageFamilyName, string packageFullName, bool isFramework = false, bool isBundle = false, bool isResourcePackage = false, bool isDevelopmentMode = false, bool isPartiallyStaged = false, string status = "")
        {
            Name = name;
            Version = version;
            Publisher = publisher;
            Architecture = architecture;
            PackageFamilyName = packageFamilyName;
            PackageFullName = packageFullName;
            IsFramework = isFramework;
            IsBundle = isBundle;
            IsResourcePackage = isResourcePackage;
            IsDevelopmentMode = isDevelopmentMode;
            IsPartiallyStaged = isPartiallyStaged;
            Status = status;
        }

        // Device associado ao perfil.  
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
