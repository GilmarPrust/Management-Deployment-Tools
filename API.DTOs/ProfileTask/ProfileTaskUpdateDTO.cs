using DCM.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.ProfileTask
{
    /// <summary>
    /// DTO para atualização de uma tarefa de perfil (ProfileTask).
    /// </summary>
    public class ProfileTaskUpdateDTO
    {
        /// <summary>
        /// Nome da tarefa de perfil.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição da tarefa de perfil.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Indica se a tarefa está habilitada.
        /// </summary>
        [Required]
        public bool Enabled { get; set; }

        /// <summary>
        /// Fase da tarefa de perfil.
        /// </summary>
        [Required]
        public ProfileTaskPhase Phase { get; init; }
    }
}
