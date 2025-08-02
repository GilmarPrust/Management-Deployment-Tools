namespace API.Control.DTOs.DriverPackOEM
{
    /// <summary>
    /// DTO para leitura de pacote de driver OEM.
    /// </summary>
    public class DriverPackOEMReadDTO
    {
        /// <summary>
        /// Identificador único do pacote de driver OEM.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome do arquivo do pacote de driver OEM.
        /// </summary>
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Sistema operacional suportado pelo pacote de driver OEM.
        /// </summary>
        public string OS { get; init; } = string.Empty;

        /// <summary>
        /// Versão do pacote de driver OEM.
        /// </summary>
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Caminho de origem do pacote de driver OEM.
        /// </summary>
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Hash do pacote de driver OEM.
        /// </summary>
        public string Hash { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o pacote de driver OEM está habilitado.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Identificador do modelo de dispositivo associado ao pacote de driver OEM.
        /// </summary>
        public Guid DeviceModelId { get; init; }
    }
}
