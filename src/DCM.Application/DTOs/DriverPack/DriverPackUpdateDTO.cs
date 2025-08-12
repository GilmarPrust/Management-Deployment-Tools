using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.DriverPack
{
    /// <summary>
    /// DTO para atualização de pacote de driver.
    /// </summary>
    public class DriverPackUpdateDTO
    {
        /// <summary>
        /// Nome do arquivo do pacote de driver.
        /// </summary>
        [Required]
        public string FileName { get; init; } = string.Empty;

        /// <summary>
        /// Sistema operacional suportado pelo pacote de driver.
        /// </summary>
        [Required]
        public string OS { get; init; } = string.Empty;

        /// <summary>
        /// Versão do pacote de driver.
        /// </summary>
        [Required]
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Caminho de origem do pacote de driver.
        /// </summary>
        [Required]
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Hash do pacote de driver.
        /// </summary>
        [Required]
        public string Hash { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o pacote de driver está habilitado.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Identificador do modelo de dispositivo associado ao pacote de driver OEM.
        /// </summary>
        [Required]
        public Guid DeviceModelId { get; init; }

        /// <summary>
        /// Indica se o pacote de driver é OEM.
        /// </summary>
        [Required]
        public required bool IsOEM { get; init; }
    }
}
