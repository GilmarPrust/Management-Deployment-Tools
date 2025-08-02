namespace API.Control.DTOs.DeviceModel.DriverPackOEM
{
    /// <summary>
    /// DTO para leitura dos pacotes de driver OEM associados a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelDriverPackOEMUpdateDTO
    {
        /// <summary>
        /// Lista dos objetos de pacotes de driver OEM associados ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<DriverPackOEMUpdateDTO> DriverPacks { get; init; } = Array.Empty<DriverPackOEMUpdateDTO>();
    }
}