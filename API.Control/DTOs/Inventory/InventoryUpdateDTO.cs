namespace API.Control.DTOs.Inventory
{
    public class InventoryUpdateDTO
    {
        [Required]
        public Dictionary<string, string> Data { get; set; } = new();
    }
}
