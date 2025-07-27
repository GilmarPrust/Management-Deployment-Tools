namespace API.Control.DTOs.Inventory
{
    public class InventoryCreateDTO
    {
        [Required]
        public Guid DeviceId { get; init; } = Guid.Empty;        
    }
}
