namespace API.DTOs.AppxPackage.Devices
{
    /// <summary>
    /// DTO para atualização das aplicações associadas a um perfil de implantação.
    /// </summary>
    public class AppxPackageDevicesUpdateDTO
    {
        /// <summary>
        /// Lista de IDs das aplicações que devem ser associadas ao perfil de implantação.
        /// </summary>
        public IReadOnlyList<Guid> DeviceIds { get; init; } = Array.Empty<Guid>();
    }
}
