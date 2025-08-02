namespace API.Control.DTOs.DriverPack
{
    /// <summary>
    /// DTO para criação de pacote de driver.
    /// </summary>
    public class DriverPackCreateDTO
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
    }
}
