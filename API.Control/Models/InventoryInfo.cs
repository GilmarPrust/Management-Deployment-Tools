using System;
using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Informações detalhadas de inventário.
    /// </summary>
    public class InventoryInfo
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "O campo InventoryId é obrigatório.")]
        public required Guid InventoryId { get; set; }

        [Required(ErrorMessage = "A data/hora é obrigatória.")]
        public required DateTime DateTime { get; set; } = DateTime.UtcNow;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para EF
        public InventoryInfo() { }

        public virtual Inventory Inventory { get; set; } = null!;
    }
}
