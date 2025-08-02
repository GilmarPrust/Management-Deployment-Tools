namespace API.Control.DTOs.Firmware
{
    /// <summary>
    /// DTO para leitura de firmware.
    /// </summary>
    public class FirmwareReadDTO
    {
        /// <summary>
        /// Identificador único do firmware.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome do arquivo do firmware.
        /// </summary>
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Versão do firmware.
        /// </summary>
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Caminho de origem do firmware.
        /// </summary>
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Hash do arquivo do firmware.
        /// </summary>
        public string Hash { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o firmware está habilitado.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Identificador do modelo de dispositivo associado ao firmware.
        /// </summary>
        public Guid DeviceModelId { get; init; }
    }
}
