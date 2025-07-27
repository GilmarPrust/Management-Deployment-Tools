namespace API.Control.Entities
{
    /// <summary>
    /// Representa um inventário vinculado a um dispositivo.
    /// </summary>
    public class Inventory : _BaseEntity
    {
        [Required]
        public required Guid DeviceId { get; init; }

        public Inventory() { }

        public virtual Device Device { get; set; } = null!;

        public Dictionary<string, string>? Hardware { get; set; } = null;


    }
}
