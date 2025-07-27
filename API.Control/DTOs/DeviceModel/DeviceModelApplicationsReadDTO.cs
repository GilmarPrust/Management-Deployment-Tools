namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelApplicationsReadDTO
    {
        public List<Guid> ApplicationIds { get; init; } = new();

    }
}
