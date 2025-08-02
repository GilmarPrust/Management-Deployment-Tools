namespace API.Control.DTOs.Device
{
    /// <summary>
    /// DTO para criação de dispositivo.
    /// </summary>
    public class DeviceCreateDTO
    {
        /// <summary>
        /// Nome do computador do dispositivo.
        /// </summary>
        [Required]
        public string ComputerName { get; set; } = string.Empty;

        /// <summary>
        /// Número de série do dispositivo.
        /// </summary>
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Endereço MAC do dispositivo.
        /// </summary>
        [Required]
        public string MacAddress { get; set; } = string.Empty;

        /// <summary>
        /// ID do modelo de dispositivo.
        /// </summary>
        [Required]
        public Guid DeviceModelId { get; set; }
    }
}
