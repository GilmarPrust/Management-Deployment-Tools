namespace API.Control.Entities.Auxiliary
{
    /// <summary>
    /// Representa um fabricante de dispositivos.
    /// </summary>
    public class Manufacturer
    {
        /// <summary>
        /// Identificador único da arquitetura.
        /// </summary>
        public Guid Id { get; init; } = Guid.NewGuid();

        /// <summary>
        /// Nome completo do fabricante.
        /// </summary>
        [Required, StringLength(50)]
        public required string Name { get; init; }

        /// <summary>
        /// Nome curto do fabricante.
        /// </summary>
        [Required, StringLength(50)]
        public required string ShortName { get; init; }

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public Manufacturer() { }
    }
}
