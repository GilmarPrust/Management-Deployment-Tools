namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelFirmwareReadDTO
    {
        public FirmwareReadDTO? Firmware { get; init; } = null;
        public List<DriverPackReadDTO>? DriverPacks { get; init; } = null;
        public List<Guid> ApplicationIds { get; init; } = new();
        public List<Guid> DeviceIds { get; init; } = new();

    }
}
