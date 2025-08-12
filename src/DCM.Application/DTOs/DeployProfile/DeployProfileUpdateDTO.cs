using DCM.Application.DTOs.ProfileTask;
using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.DeployProfile
{
    /// <summary>
    /// DTO para atualização de perfil de implantação.
    /// </summary>
    public class DeployProfileUpdateDTO
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
        /// Indica se o perfil está habilitado.
        /// </summary>
        [Required]
        public bool Enabled { get; init; }

        /// <summary>
        /// ID da imagem associada ao perfil.
        /// </summary>
        [Required]
        public Guid ImageId { get; init; }

        /// <summary>
        /// Tarefa de perfil associada.
        /// </summary>
        public IReadOnlyList<ProfileTaskReadDTO> ProfileTasks { get; init; } = Array.Empty<ProfileTaskReadDTO>();

        /// <summary> 
        /// Lista de IDs das aplicações associadas ao perfil.
        /// </summary>
        public IReadOnlyList<Guid> ApplicationIds { get; init; } = Array.Empty<Guid>();
    }
}
