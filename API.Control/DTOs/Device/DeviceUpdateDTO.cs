namespace API.Control.DTOs.Device
{
    public class DeviceUpdateDTO
    {
        [Required]
        public string ComputerName { get; set; } = string.Empty;

        [Required]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public string MacAddress { get; set; } = string.Empty;

        [Required]
        public Guid DeviceModelId { get; set; }

        [Required]
        public bool Enabled { get; init; }

        public Guid? DeployProfileId { get; init; } = null;
        public List<Guid> ApplicationIds { get; init; } = new();
        public List<Guid> AppxPackageIds { get; init; } = new();
        public List<Guid> DriverPackIds { get; init; } = new();
    }
}
