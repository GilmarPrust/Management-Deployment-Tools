using System.ComponentModel.DataAnnotations;

namespace API.Control2.DTOs
{
    public class InventoryDTO
    {
        public Guid Id { get; set; }

        public string Info { get; set; } = string.Empty;

        [Required]
        public Guid DeviceId { get; set; }
    }
}
