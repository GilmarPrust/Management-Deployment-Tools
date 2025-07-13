using System.ComponentModel.DataAnnotations;

namespace API.Control2.DTOs
{
    public class ProfileDeployReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public Guid ImageId { get; init; } = Guid.Empty;
        public bool Enabled { get; init; }

        public List<String> SourcePath { get; init; } = new();
        public List<Guid> ApplicationIds { get; init; } = new();
        public List<Guid> DeviceIds { get; init; } = new();

    }
}
