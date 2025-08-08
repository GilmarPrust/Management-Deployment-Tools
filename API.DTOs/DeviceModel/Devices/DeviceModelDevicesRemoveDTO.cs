namespace API.DTOs.DeviceModel.Devices
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelDevicesRemoveDTO
    {
        /// <summary>
        /// ID do dispositivo a ser associado ao modelo de dispositivo.
        /// </summary>
        public Guid DeviceId { get; init; }

    }
}
