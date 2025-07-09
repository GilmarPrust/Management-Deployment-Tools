using API.Control.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs
{
    public class DeviceModel_CreateDTO
    {
        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer { get; init; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; init; } = string.Empty;
        public string Type { get; init; } = string.Empty;
        public Guid FirmwareId { get; init; } = Guid.Empty;

        public List<Guid> ApplicationsId { get; init; } = new();
        public List<Guid> DriverPacksId { get; init; } = new();
    }

    public class DeviceModel_ReadDTO
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Manufacturer { get; init; } = string.Empty;
        public string Model { get; init; } = string.Empty;
        public string Type { get; init; } = string.Empty;
        public Guid FirmwareId { get; init; } = Guid.Empty;
        public List<Guid> ApplicationsId { get; init; } = new List<Guid>();
        public List<Guid> DriverPacksId { get; init; } = new List<Guid>();
    }
    public class DeviceModel_UpdateDTO
    {
        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
