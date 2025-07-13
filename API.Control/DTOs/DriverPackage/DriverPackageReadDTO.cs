using API.Control.DTOs.Application;

namespace API.Control.DTOs.DriverPackage
{
    public class DriverPackageReadDTO
    {
        public Guid Id { get; init; }
        public string FileName { get; init; } = string.Empty;
        public string OS { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string Hash { get; init; } = string.Empty;
        public bool Enabled { get; init; }

        public List<DriverPackageReadDTO> DriverPackages { get; init; } = new();
    }
}
