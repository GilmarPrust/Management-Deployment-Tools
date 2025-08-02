namespace API.Control.DTOs.Inventory
{
    /// <summary>
    /// DTO para leitura de inventário vinculado a um dispositivo.
    /// </summary>
    public class InventoryReadDTO
    {
        /// <summary>
        /// Identificador único do inventário.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Identificador do dispositivo ao qual o inventário está vinculado.
        /// </summary>
        public Guid DeviceId { get; init; }

        /// <summary>
        /// Dados de hardware do inventário.
        /// </summary>
        public Dictionary<string, string> Hardware { get; init; } = new();

        /// <summary>
        /// Dados de softwares do inventário.
        /// </summary>
        public Dictionary<string, string> Softwares { get; init; } = new();
    }
}
