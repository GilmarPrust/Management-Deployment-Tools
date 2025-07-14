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

        [Required]
        public Guid InventoryId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para EF
        public InventoryInfo() { }
    }
}
