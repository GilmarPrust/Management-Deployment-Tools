using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Inventory
{
    /// <summary>
    /// DTO para criação de inventário vinculado a um dispositivo.
    /// </summary>
    public class InventoryCreateDTO
    {
        /// <summary>
        /// Identificador do dispositivo ao qual o inventário será vinculado.
        /// </summary>
        [Required]
        public Guid DeviceId { get; init; }

        /// <summary>
        /// Dados de hardware do inventário (opcional).
        /// </summary>
        public Dictionary<string, string>? Hardware { get; init; }

        /// <summary>
        /// Dados de softwares do inventário (opcional).
        /// </summary>
        public Dictionary<string, string>? Softwares { get; init; }
    }
}
