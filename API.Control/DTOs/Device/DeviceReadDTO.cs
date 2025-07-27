namespace API.Control.DTOs.Device
{
    /// <summary>
    /// DTO para leitura de dispositivo.
    /// </summary>
    public class DeviceReadDTO
    {
        public Guid Id { get; init; }
        public string ComputerName { get; init; } = string.Empty;
        public string SerialNumber { get; init; } = string.Empty;
        public string MacAddress { get; init; } = string.Empty;
        public bool Enabled { get; init; }
        public DeviceModelReadDTO DeviceModel { get; init; } = null!;
        public InventoryReadDTO? Inventory { get; init; } = null;


        public Guid? DeployProfileId { get; init; } = null;
        public List<Guid> ApplicationIds { get; init; } = new();
        public List<Guid> AppxPackageIds { get; init; } = new();
        public List<Guid> DriverPackIds { get; init; } = new();

    }
}
