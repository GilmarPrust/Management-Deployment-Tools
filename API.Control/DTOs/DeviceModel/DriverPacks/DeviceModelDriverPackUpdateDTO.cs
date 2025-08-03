namespace API.Control.DTOs.DeviceModel.DriverPacks
{
    /// <summary>
    /// DTO para leitura dos pacotes de driver OEM associados a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelDriverPackUpdateDTO
    {
        /// <summary>
        /// Lista dos objetos de pacotes de driver OEM associados ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<DriverPackUpdateDTO> DriverPacks { get; init; } = Array.Empty<DriverPackUpdateDTO>();
    }
}