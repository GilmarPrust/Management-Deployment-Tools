namespace DCM.Application.DTOs.Inventory
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

    }
}
