namespace API.Control.Entities
{
    /// <summary>
    /// Representa um pacote Appx associado a dispositivos.
    /// </summary>
    public class AppxPackage : _BaseEntity
    {
        [Required, StringLength(100)]
        public required string Name { get; set; }

        [Required, StringLength(50)]
        public required string Version { get; set; }

        [Required, StringLength(100)]
        public required string Publisher { get; set; }

        [StringLength(50)]
        public string Architecture { get; set; } = string.Empty;

        [StringLength(100)]
        public string PackageFamilyName { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public required string PackageFullName { get; set; }

        public bool IsFramework { get; set; } = false;
        public bool IsBundle { get; set; } = false;
        public bool IsResourcePackage { get; set; } = false;
        public bool IsDevelopmentMode { get; set; } = false;
        public bool IsPartiallyStaged { get; set; } = false;

        [StringLength(100)]
        public string Status { get; set; } = string.Empty;


        public AppxPackage() { }

        
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}

// implementar Grupos Appx.