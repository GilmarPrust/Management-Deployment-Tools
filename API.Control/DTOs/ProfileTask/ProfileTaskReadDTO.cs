using API.Control.DTOs.Image;

namespace API.Control.DTO.Deploy.Task
{
    /// <summary>
    /// DTO para leitura de perfil de implantação.
    /// </summary>
    public class ProfileTaskReadDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool Enabled { get; init; }

        public ImageReadDTO? Image { get; init; } = null;
    }
}


