namespace API.Control.Models
{
    public class Inventory
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required Guid DeviceId { get; init; }
        public required virtual Device Device { get; set; }
        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public Inventory() { }


        public virtual ICollection<InventoryInfo> InventoryInfos { get; set; } = new List<InventoryInfo>();

    }
}
