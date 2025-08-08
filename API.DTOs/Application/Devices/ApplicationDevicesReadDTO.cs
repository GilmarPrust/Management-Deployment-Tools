namespace API.DTOs.Application.Devices
{
    /// <summary>
    /// DTO para leitura dos dispositivos vinculados a uma aplicação.
    /// </summary>
    public class ApplicationDevicesReadDTO
    {
        /// <summary>
        /// Lista de IDs dos dispositivos vinculados.
        /// </summary>
        public IReadOnlyList<Guid> DeviceIds { get; init; } = Array.Empty<Guid>();
    }
}
