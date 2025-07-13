using API.Control.DTOs.Device;
using API.Control.DTOs.DriverPackage;
using API.Control.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.AppxPackage
{
    public class AppxPackageReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string Publisher { get; init; } = string.Empty;
        public string Architecture { get; init; } = string.Empty;
        public string PackageFamilyName { get; init; } = string.Empty;
        public string PackageFullName { get; init; } = string.Empty;
        public bool IsFramework { get; init; }
        public bool IsBundle { get; init; }
        public bool IsResourcePackage { get; init; }
        public bool IsDevelopmentMode { get; init; }
        public bool IsPartiallyStaged { get; init; }
        public string Status { get; init; } = string.Empty;
        public bool Enabled { get; init; }

        public List<DeviceReadDTO> Devices { get; init; } = new();

    }
}
