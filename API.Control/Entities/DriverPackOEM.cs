namespace API.Control.Entities
{
    /// <summary>
    /// Representa um pacote de driver OEM associado a um modelo de dispositivo.
    /// </summary>
    public class DriverPackOEM : DriverPack
    {
        /// <summary>
        /// ID do modelo de dispositivo ao qual o pacote de driver OEM está vinculado.
        /// </summary>
        [Required]
        public required Guid DeviceModelId { get; init; }

        /// <summary>
        /// Modelo de dispositivo associado ao pacote de driver OEM.
        /// </summary>
        public virtual DeviceModel DeviceModel { get; init; } = null!;
    }
}
