namespace API.Control.DTOs.Inventory
{
    public class InventoryReadDTO
    {
        public Guid Id { get; init; }

        public Guid DeviceId { get; init; } = Guid.Empty;

        public Dictionary<string, string> Data { get; init; } = new();
    }
}
