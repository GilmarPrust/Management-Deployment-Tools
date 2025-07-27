namespace API.Control.DTOs.DeployProfile
{
    /// <summary>
    /// DTO para leitura de perfil de implantação.
    /// </summary>
    public class DeployProfileReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool Enabled { get; init; }
        public ManufacturerReadDTO Image { get; init; } = null!;
        public List<Guid> ApplicationIds { get; init; } = new();
        public List<Guid> DeviceIds { get; init; } = new();
        public List<String> SourcePath { get; set; } = new List<String>();
        public ProfileTaskReadDTO? ProfileTasks { get; set; } = null;

    }
}


