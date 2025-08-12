using DCM.Application.DTOs.DriverPack;

namespace DCM.Application.DTOs.DeviceModel.DriverPacks
{
    /// <summary>
    /// DTO para leitura dos pacotes de driver OEM associados a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelDriverPackReadDTO
    {
        /// <summary>
        /// Lista dos pacotes de driver OEM associados ao modelo de dispositivo.
        /// </summary>
        public IReadOnlyList<DriverPackReadDTO> DriverPacks { get; init; } = Array.Empty<DriverPackReadDTO>();
    }
}
