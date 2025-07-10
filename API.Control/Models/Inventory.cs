namespace API.Control.Models
{
    public class Inventory
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Info { get; set; } = string.Empty;

        public required Guid DeviceId { get; set; } = Guid.Empty;
        public required virtual Device Device { get; set; }
    }
}
