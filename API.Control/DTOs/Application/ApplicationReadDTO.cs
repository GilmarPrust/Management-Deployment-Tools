using API.Control.DTOs.Device;
using API.Control.DTOs.DeviceModel;
using API.Control.DTOs.ProfileDeploy;

namespace API.Control.DTOs.Application
{
    /// <summary>
    /// DTO para leitura de aplicativo.
    /// </summary>
    public class ApplicationReadDTO
    {
        public Guid Id { get; init; }
        public string NameID { get; init; } = string.Empty;
        public string DisplayName { get; init; } = string.Empty;
        public string Version { get; init; } = string.Empty;
        public string FileName { get; init; } = string.Empty;
        public string Argument { get; init; } = string.Empty;
        public string Source { get; init; } = string.Empty;
        public string Filter { get; init; } = string.Empty;
        public string Hash { get; init; } = string.Empty;
        public bool Enabled { get; init; }

        public List<DeviceReadDTO> Devices { get; init; } = new();
        public List<DeviceModelReadDTO> DeviceModels { get; init; } = new();
        public List<ProfileDeployReadDTO> ProfileDeploys { get; init; } = new();
    }
}
