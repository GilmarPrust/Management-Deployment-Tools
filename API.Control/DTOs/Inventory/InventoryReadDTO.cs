using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.Inventory
{
    public class InventoryReadDTO
    {
        public Guid Id { get; init; }

        public Guid DeviceId { get; init; } = Guid.Empty;

        public string Info { get; init; } = string.Empty;
    }
}
