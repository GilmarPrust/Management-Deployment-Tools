using System.ComponentModel.DataAnnotations;
using DCM.Core.Enums;

namespace DCM.Application.DTOs.ProfileTask
{
    /// <summary>
    /// DTO para criação de uma tarefa de perfil (ProfileTask).
    /// </summary>
    public class ProfileTaskCreateDTO
    {
        /// <summary>
        /// Nome da tarefa de perfil.
        /// </summary>
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da tarefa de perfil.
        /// </summary>
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Lista de IDs dos perfis de implantação associados à tarefa.
        /// </summary>
        [Required]
        public IReadOnlyList<Guid> DeployProfileIds { get; init; } = [];

        /// <summary>
        /// Fase da tarefa de perfil.
        /// </summary>
        [Required]
        public ProfileTaskPhase Phase { get; init; }
    }
}
