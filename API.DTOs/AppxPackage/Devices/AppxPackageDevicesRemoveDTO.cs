namespace API.DTOs.AppxPackage.Devices
{
    /// <summary>
    /// DTO para remoção de aplicações de um perfil de implantação.
    /// </summary>
    public class DeployProfileDevicesRemoveDTO
    {
        /// <summary>
        /// Identificador único do dispositivo a ser atualizado.
        /// </summary>
        public Guid DeviceId { get; init; }
    }
}
