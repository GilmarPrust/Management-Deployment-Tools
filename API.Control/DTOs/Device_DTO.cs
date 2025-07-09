using API.Control.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs
{
    public class Device_ReadDTO
    {
        public Guid Id { get; init; }
        public string ComputerName { get; init; } = string.Empty;
        public string SerialNumber { get; init; } = string.Empty;
        public string MacAddress { get; init; } = string.Empty;
        public Guid DeviceModelId { get; init; } = Guid.Empty;
        public Guid InventoryId { get; init; } = Guid.Empty;
        public Guid ProfileId { get; init; } = Guid.Empty;
        public List<Guid> ApplicationsId { get; init; } = new List<Guid>();

    }

    public class Device_WriteDTO
    {
        [Required(ErrorMessage = "Computer name is required.")]
        public string ComputerName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "MAC Address is required.")]
        public string MacAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Device model Id is required.")]
        public Guid DeviceModelId { get; set; } = Guid.Empty;
    }
 
    public class Device_UpdateDTO
    {
        [Required(ErrorMessage = "Computer name is required.")]
        public string ComputerName { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "MAC Address is required.")]
        public string MacAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Device model Id is required.")]
        public Guid DeviceModelId { get; set; } = Guid.Empty;
    }
}
