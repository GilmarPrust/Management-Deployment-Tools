namespace DCM.Application.DTOs.Application.DeployProfiles
{
    /// <summary>
    /// DTO para leitura dos perfis de implantação vinculados a uma aplicação.
    /// </summary>
    public class ApplicationDeployProfilesReadDTO
    {
        /// <summary>
        /// Lista de IDs dos perfis de implantação vinculados.
        /// </summary>
        public IReadOnlyList<Guid> DeployProfileIds { get; init; } = Array.Empty<Guid>();
    }
}
