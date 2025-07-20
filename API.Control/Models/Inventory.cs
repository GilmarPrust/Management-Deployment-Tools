using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um inventário vinculado a um dispositivo.
    /// </summary>
    public class Inventory
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "O campo DeviceId é obrigatório.")]
        public required Guid DeviceId { get; init; }
        public virtual Device Device { get; set; } = null!;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public Inventory() { }

        public virtual ICollection<InventoryInfo> Infos { get; set; } = new List<InventoryInfo>();
    }
}
