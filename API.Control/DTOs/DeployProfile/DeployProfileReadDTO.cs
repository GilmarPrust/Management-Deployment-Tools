using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.ProfileDeploy
{
    /// <summary>
    /// DTO para leitura de perfil de implantação.
    /// </summary>
    public class DeployProfileReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public Guid ImageId { get; init; }
        public bool Enabled { get; init; }
        public List<Guid> ApplicationIds { get; init; } = new();
        public List<Guid> DeviceIds { get; init; } = new();
    }
}
