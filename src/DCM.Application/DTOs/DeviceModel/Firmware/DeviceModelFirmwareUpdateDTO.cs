using DCM.Application.DTOs.Firmware;
using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.DeviceModel.Firmware
{
    /// <summary>
    /// DTO para atualização do firmware associado a um modelo de dispositivo.
    /// </summary>
    public class DeviceModelFirmwareUpdateDTO
    {
        /// <summary>
        /// Firmware que deve ser associado ao modelo de dispositivo.
        /// </summary>
        [Required]
        public FirmwareUpdateDTO? Firmware { get; init; } = null;
    }
}