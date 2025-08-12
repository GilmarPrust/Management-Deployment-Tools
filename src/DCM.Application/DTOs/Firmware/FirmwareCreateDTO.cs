using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.Firmware
{
    /// <summary>
    /// DTO para criação de firmware.
    /// </summary>
    public class FirmwareCreateDTO
    {
        /// <summary>
        /// Nome do arquivo do firmware.
        /// </summary>
        [Required]
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Versão do firmware.
        /// </summary>
        [Required]
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Caminho de origem do firmware.
        /// </summary>
        [Required]
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Hash do arquivo do firmware.
        /// </summary>
        [Required]
        public string Hash { get; init; } = string.Empty;

        /// <summary>
        /// Identificador do modelo de dispositivo associado ao firmware.
        /// </summary>
        [Required]
        public Guid DeviceModelId { get; init; }
    }
}
