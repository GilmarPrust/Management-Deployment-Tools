namespace API.Control.DTOs.DeviceModel.Applications
{
    /// <summary>
    /// DTO para leitura das aplicações associadas a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelApplicationsReadDTO
    {
        /// <summary>
        /// Lista de IDs das aplicações associadas ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<Guid> ApplicationIds { get; init; } = Array.Empty<Guid>();
    }
}
