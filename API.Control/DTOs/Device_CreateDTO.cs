using API.Control.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs
{
    public class Device_CreateDTO
    {
        public string ComputerName { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string MacAddress { get; set; } = string.Empty;
        public Guid DeviceModelId { get; set; }
    }
}
