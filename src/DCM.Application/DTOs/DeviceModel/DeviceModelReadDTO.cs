namespace DCM.Application.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para leitura de modelo de dispositivo.
    /// </summary>
    public class DeviceModelReadDTO
    {
        /// <summary>
        /// Identificador único do modelo de dispositivo.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome do fabricante do modelo de dispositivo.
        /// </summary>
        public string Manufacturer { get; init; } = string.Empty;

        /// <summary>
        /// Nome do modelo do dispositivo.
        /// </summary>
        public string Model { get; init; } = string.Empty;

        /// <summary>
        /// Tipo do modelo de dispositivo.
        /// </summary>
        public string Type { get; init; } = string.Empty;
    }
}
