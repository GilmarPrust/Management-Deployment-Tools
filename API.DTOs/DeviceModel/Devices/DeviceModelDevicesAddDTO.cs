namespace API.DTOs.DeviceModel.Devices
{
    /// <summary>
    /// DTO para adição de dispositivos a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelDevicesAddDTO
    {
        /// <summary>
        /// ID do dispositivo a ser associado ao modelo de dispositivo.
        /// </summary>
        public Guid DeviceId { get; init; }
    }
}
