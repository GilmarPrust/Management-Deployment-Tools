using API.Control.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs
{
    public class Device_WriteDTO
    {
        public string ComputerName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public Guid DeviceModelId { get; set; }
    }
    public class Device_ReadDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string ComputerName { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public string MacAddress { get; set; } = string.Empty;


        [Required]
        public Guid DeviceModelId { get; set; } = Guid.Empty;

        public Guid? InventoryId { get; set; } = null;
        public Guid? ProfileId { get; set; } = null;
        public List<Guid>? ApplicationsId { get; set; } = new List<Guid>();
    }
}
