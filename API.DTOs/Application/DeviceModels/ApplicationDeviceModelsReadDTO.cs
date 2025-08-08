namespace API.DTOs.Application.DeviceModels
{
    /// <summary>
    /// DTO para leitura dos modelos de dispositivo vinculados a uma aplicação.
    /// </summary>
    public class ApplicationDeviceModelsReadDTO
    {
        /// <summary>
        /// Lista de IDs dos modelos de dispositivo vinculados.
        /// </summary>
        public IReadOnlyList<Guid> DeviceModelIds { get; init; } = Array.Empty<Guid>();
    }
}
