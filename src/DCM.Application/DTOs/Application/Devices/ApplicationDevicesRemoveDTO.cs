namespace DCM.Application.DTOs.Application.Devices
{
    /// <summary>
    /// DTO para remoção de dispositivos vinculados a uma aplicação.
    /// </summary>
    public class ApplicationDevicesRemoveDTO
    {
        /// <summary>
        /// IDs do dispositivo vinculado.
        /// </summary>
        public Guid DeviceId { get; init; }
    }
}
