using System.ComponentModel.DataAnnotations;

namespace API.Control2.DTOs
{
    public class InventoryReadDTO
    {
        public Guid Id { get; init; }

        public Guid DeviceId { get; init; } = Guid.Empty;

        public string Info { get; init; } = string.Empty;
    }
}
