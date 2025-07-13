using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.Inventory
{
    public class InventoryUpdateDTO
    {
        [Required]
        public string Info { get; set; } = string.Empty;

    }
}
