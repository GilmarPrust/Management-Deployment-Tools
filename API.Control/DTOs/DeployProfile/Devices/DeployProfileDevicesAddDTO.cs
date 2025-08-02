namespace API.Control.DTOs.DeployProfile.Applications
{
    /// <summary>
    /// DTO para adição de aplicações a um perfil de implantação.
    /// </summary>
    public class AppxPackageDevicesAddDTO
    {
        /// <summary>
        /// Identificador único do dispositivo a ser atualizado.
        /// </summary>
        public Guid DeviceId { get; init; }
    }
}
