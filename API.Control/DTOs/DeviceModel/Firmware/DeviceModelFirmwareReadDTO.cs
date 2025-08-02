namespace API.Control.DTOs.DeviceModel.Firmware
{
    /// <summary>
    /// DTO para leitura do firmware associado a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelFirmwareReadDTO
    {
        /// <summary>
        /// Firmware associado ao modelo de dispositivo.
        /// </summary>
        public FirmwareReadDTO? Firmware { get; init; } = null;
    }
}
