namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelDevicesReadDTO
    {
        public List<Guid> DeviceIds { get; init; } = new();

    }
}
