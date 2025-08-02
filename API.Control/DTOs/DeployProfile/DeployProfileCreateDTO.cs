namespace API.Control.DTOs.DeployProfile
{
    /// <summary>
    /// DTO para criação de perfil de implantação.
    /// </summary>
    public class DeployProfileCreateDTO
    {
        /// <summary>
        /// Nome do perfil de implantação.
        /// </summary>
        [Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Descrição do perfil de implantação.
        /// </summary>
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// ID da imagem associada ao perfil.
        /// </summary>
        [Required]
        public Guid ImageId { get; init; }

        /// <summary>
        /// ProfileTasks associadas ao perfil de implantação.
        /// </summary>
        public IReadOnlyList<ProfileTaskReadDTO> ProfileTasks { get; init; } = Array.Empty<ProfileTaskReadDTO>();
    }
}
