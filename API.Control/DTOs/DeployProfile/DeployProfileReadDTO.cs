namespace API.Control.DTOs.DeployProfile
{
    /// <summary>
    /// DTO para leitura de perfil de implantação.
    /// </summary>
    public class DeployProfileReadDTO
    {
        /// <summary>
        /// Identificador único do perfil.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome do perfil de implantação.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Descrição do perfil de implantação.
        /// </summary>
        public string Description { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o perfil está habilitado.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Imagem associada ao perfil.
        /// </summary>
        public ImageReadDTO Image { get; init; } = null!;

        /// <summary>
        /// Tarefas de perfil associada.
        /// </summary>
        public IReadOnlyList<ProfileTaskReadDTO> ProfileTasks { get; init; } = Array.Empty<ProfileTaskReadDTO>();

        /// <summary>
        /// Lista de IDs das aplicações associadas.
        /// </summary>
        public IReadOnlyList<Guid> ApplicationIds { get; init; } = Array.Empty<Guid>();

    }
}


