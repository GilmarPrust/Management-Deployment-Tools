namespace API.Control.Entities.Auxiliary
{
    /// <summary>
    /// Representa um sistema operacional, incluindo nome, versão, arquitetura e identificadores.
    /// </summary>
    public class OperatingSystem
    {
        /// <summary>
        /// Identificador único do sistema operacional.
        /// </summary>
        public Guid Id { get; init; } = Guid.NewGuid();

        /// <summary>
        /// Nome do sistema operacional.
        /// </summary>
        [Required, StringLength(50)]
        public required string Name { get; init; } = string.Empty;

        /// <summary>
        /// Nome curto do sistema operacional.
        /// </summary>
        [Required, StringLength(5)]
        public required string ShortName { get; init; } = string.Empty;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public OperatingSystem() { }
    }
}
