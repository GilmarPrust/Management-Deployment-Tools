using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.DeployProfile.Devices
{
    /// <summary>
    /// DTO para adição de aplicações a um perfil de implantação.
    /// </summary>
    public class AppxPackageDevicesAddDTO
    {
        /// <summary>
        /// Identificador único do dispositivo a ser atualizado.
        /// </summary>
        [Required]
        public Guid DeviceId { get; init; }
    }
}
