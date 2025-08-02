namespace API.Control.Entities
{
    /// <summary>
    /// Representa um pacote de driver associado a um modelo de dispositivo.
    /// </summary>
    public class DriverPack : BaseEntity
    {
        /// <summary>
        /// Nome do arquivo do pacote de driver.
        /// </summary>
        [Required, StringLength(100)]
        public required string FileName { get; set; }

        /// <summary>
        /// Sistema operacional suportado pelo pacote de driver.
        /// </summary>
        [Required, StringLength(50)]
        public required string OS { get; set; }

        /// <summary>
        /// Versão do pacote de driver.
        /// </summary>
        [Required, StringLength(50)]
        public required string Version { get; set; }

        /// <summary>
        /// Caminho de origem do pacote de driver.
        /// </summary>
        [Required, StringLength(200)]
        public required string Source { get; set; }

        /// <summary>
        /// Hash do pacote de driver.
        /// </summary>
        [Required, StringLength(64)]
        public required string Hash { get; set; }

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public DriverPack() { }
    }
}
