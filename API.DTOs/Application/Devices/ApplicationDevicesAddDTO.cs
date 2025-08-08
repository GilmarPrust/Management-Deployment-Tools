namespace API.DTOs.Application.Devices
{
    /// <summary>
    /// DTO para adicionar vincular um dispositivo a uma aplicação.
    /// </summary>
    public class ApplicationDevicesAddDTO
    {
        /// <summary>
        /// IDs do dispositivo vinculado.
        /// </summary>
        public Guid DeviceId { get; init; }
    }
}
