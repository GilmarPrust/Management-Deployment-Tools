namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelReadDTO
    {
        public Guid Id { get; init; }
        public string Manufacturer { get; init; } = string.Empty;
        public string Model { get; init; } = string.Empty;
        public string Type { get; init; } = string.Empty;

    }
}
