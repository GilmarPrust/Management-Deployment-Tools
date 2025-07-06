namespace API.Control.Models
{
    public class Inventory
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Info { get; set; } = string.Empty;

        // Device associado ao inventário.
        public required Guid DeviceId { get; set; }
        public required Device Device { get; set; }
    }
}
