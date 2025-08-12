namespace DCM.Application.DTOs.DeployProfile.Devices
{
    /// <summary>
    /// DTO para leitura das aplicações associadas a um perfil de implantação.
    /// </summary>
    public class AppxPackageDevicesReadDTO
    {
        /// <summary>
        /// Lista de IDs das aplicações associadas ao perfil de implantação.
        /// </summary>
        public IReadOnlyList<Guid> DeviceIds { get; init; } = Array.Empty<Guid>();
    }
}
