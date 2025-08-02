namespace API.Control.DTOs.DeviceModel.DriverPackOEM
{
    /// <summary>
    /// DTO para leitura dos pacotes de driver OEM associados a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelDriverPackOEMReadDTO
    {
        /// <summary>
        /// Lista dos pacotes de driver OEM associados ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<DriverPackOEMReadDTO> DriverPacks { get; init; } = Array.Empty<DriverPackOEMReadDTO>();
    }
}
