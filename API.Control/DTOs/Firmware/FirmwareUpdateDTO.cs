namespace API.Control.DTOs.Firmware
{
    /// <summary>
    /// DTO para atualização de firmware.
    /// </summary>
    public class FirmwareUpdateDTO
    {
        /// <summary>
        /// Indica se o firmware está habilitado.
        /// </summary>
        public bool Enabled { get; init; }
    }
}
