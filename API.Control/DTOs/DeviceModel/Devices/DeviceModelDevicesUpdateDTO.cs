namespace API.Control.DTOs.DeviceModel.Devices
{
    /// <summary>
    /// DTO para atualização dos dispositivos associados a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelDevicesUpdateDTO
    {
        /// <summary>
        /// Lista de IDs dos dispositivos que devem ser associados ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<Guid> DeviceIds { get; init; } = Array.Empty<Guid>();
    }
}
