namespace API.Control.DTOs.DriverPackOEM
{
    /// <summary>
    /// DTO para criação de pacote de driver OEM.
    /// </summary>
    public class DriverPackOEMCreateDTO
    {
        /// <summary>
        /// Nome do arquivo do pacote de driver OEM.
        /// </summary>
        [Required]
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Sistema operacional suportado pelo pacote de driver OEM.
        /// </summary>
        [Required]
        public string OS { get; init; } = string.Empty;

        /// <summary>
        /// Versão do pacote de driver OEM.
        /// </summary>
        [Required]
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Caminho de origem do pacote de driver OEM.
        /// </summary>
        [Required]
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Hash do pacote de driver OEM.
        /// </summary>
        [Required]
        public string Hash { get; init; } = string.Empty;

        /// <summary>
        /// Identificador do modelo de dispositivo associado ao pacote de driver OEM.
        /// </summary>
        [Required]
        public Guid DeviceModelId { get; init; }
    }
}
