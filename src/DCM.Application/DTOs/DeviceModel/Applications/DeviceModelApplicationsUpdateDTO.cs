namespace DCM.Application.DTOs.DeviceModel.Applications
{
    /// <summary>
    /// DTO para atualização das aplicações associadas a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelApplicationsUpdateDTO
    {
        /// <summary>
        /// Lista de IDs das aplicações que devem ser associadas ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<Guid> ApplicationIds { get; init; } = Array.Empty<Guid>();
    }
}
