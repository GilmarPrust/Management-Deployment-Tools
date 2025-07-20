namespace API.Control.DTOs.AppxPackage
{
    /// <summary>
    /// DTO para leitura de pacote Appx.
    /// </summary>
    public class AppxPackageReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string Publisher { get; init; } = string.Empty;
        public string Architecture { get; init; } = string.Empty;
        public string PackageFamilyName { get; init; } = string.Empty;
        public string PackageFullName { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public bool Enabled { get; init; }
        public List<Guid> DeviceIds { get; init; } = new();
    }
}
