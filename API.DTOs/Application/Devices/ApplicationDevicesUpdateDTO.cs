namespace API.DTOs.Application.Devices
{
    /// <summary>
    /// DTO para atualização dos dispositivos vinculados a uma aplicação.
    /// </summary>
    public class ApplicationDevicesUpdateDTO
    {
        /// <summary>
        /// Lista de IDs dos dispositivos a serem vinculados.
        /// </summary>
        public IReadOnlyList<Guid> DeviceIds { get; init; } = Array.Empty<Guid>();
    }
}
