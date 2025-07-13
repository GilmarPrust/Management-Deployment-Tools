using System.Text.RegularExpressions;

namespace API.Control.Models
{
    public class InventoryInfo
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid InventoryId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        //public Dictionary<string, string> Infos { get; set; } = new();


        // Construtor vazio para EF
        public InventoryInfo() { }
    }
}
