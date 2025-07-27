namespace API.Control.DTOs.AppxPackage
{
    public class AppxPackageCreateDTO
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Version { get; set; }

        [Required]
        public required string Publisher { get; set; }
        public string Architecture { get; set; } = string.Empty;
        public string PackageFamilyName { get; set; } = string.Empty;

        [Required]
        public required string PackageFullName { get; set; }
        public bool IsFramework { get; set; } = false;
        public bool IsBundle { get; set; } = false;
        public bool IsResourcePackage { get; set; } = false;
        public bool IsDevelopmentMode { get; set; } = false;
        public bool IsPartiallyStaged { get; set; } = false;
        public string Status { get; set; } = string.Empty;

    }
}
