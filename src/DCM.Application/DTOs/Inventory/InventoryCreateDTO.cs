using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.Inventory
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


    }
}
