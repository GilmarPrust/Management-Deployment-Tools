namespace API.Control.Entities
{
    /// <summary>
    /// Representa um inventário vinculado a um dispositivo.
    /// </summary>
    public class Inventory : BaseEntity
    {
        /// <summary>
        /// ID do dispositivo ao qual o inventário está vinculado.
        /// </summary>
        [Required]
        public required Guid DeviceId { get; init; }

        /// <summary>
        /// Dispositivo associado ao inventário.
        /// </summary>
        public virtual Device Device { get; set; } = null!;

        /// <summary>
        /// Dados de hardware do inventário (ex: processador, memória, fabricante).
        /// </summary>
        public Dictionary<string, string>? Hardware { get; set; } = null;

        /// <summary>
        /// Dados de softwares do inventário (ex: sistema operacional, aplicativos instalados).
        /// </summary>
        public Dictionary<string, string>? Softwares { get; set; } = null;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public Inventory() { }
    }
}
