using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.Inventory
{
    public class InventoryCreateDTO
    {
        public Guid Id { get; init; }

        [Required]
        public Guid DeviceId { get; init; } = Guid.Empty;

        [Required]
        public string Info { get; set; } = string.Empty;
        
    }
}
