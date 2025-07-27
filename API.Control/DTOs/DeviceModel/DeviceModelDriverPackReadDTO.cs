namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelDriverPackReadDTO
    {
        public List<DriverPackReadDTO>? DriverPacks { get; init; } = null;

    }
}
