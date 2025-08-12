namespace DCM.Application.DTOs.DeviceModel.Devices
{
    /// <summary>
    /// DTO para leitura dos dispositivos associados a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelDevicesReadDTO
    {
        /// <summary>
        /// Lista de IDs dos dispositivos associados ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<Guid> DeviceIds { get; init; } = Array.Empty<Guid>();
    }
}
