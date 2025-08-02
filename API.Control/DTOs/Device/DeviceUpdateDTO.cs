namespace API.Control.DTOs.Device
{
    /// <summary>
    /// DTO para atualização de dispositivo.
    /// </summary>
    public class DeviceUpdateDTO
    {
        /// <summary>
        /// Nome do computador do dispositivo.
        /// </summary>
        [Required]
        public string ComputerName { get; init; } = string.Empty;

        /// <summary>
        /// Número de série do dispositivo.
        /// </summary>
        [Required]
        public string SerialNumber { get; init; } = string.Empty;

        /// <summary>
        /// Endereço MAC do dispositivo.
        /// </summary>
        [Required]
        public string MacAddress { get; init; } = string.Empty;

        /// <summary>
        /// ID do modelo de dispositivo.
        /// </summary>
        [Required]
        public Guid DeviceModelId { get; init; }

        /// <summary>
        /// Indica se o dispositivo está habilitado.
        /// </summary>
        [Required]
        public bool Enabled { get; init; }

        /// <summary>
        /// ID do perfil de implantação associado ao dispositivo.
        /// </summary>
        public Guid? DeployProfileId { get; init; } = null;

        /// <summary>
        /// Lista de IDs das aplicações associadas ao dispositivo.
        /// </summary>
        public IReadOnlyList<Guid> ApplicationIds { get; init; } = Array.Empty<Guid>();

        /// <summary>
        /// Lista de IDs dos pacotes Appx associados ao dispositivo.
        /// </summary>
        public IReadOnlyList<Guid> AppxPackageIds { get; init; } = Array.Empty<Guid>();

        /// <summary>
        /// Lista de IDs dos pacotes de driver associados ao dispositivo.
        /// </summary>
        public IReadOnlyList<Guid> DriverPackIds { get; init; } = Array.Empty<Guid>();
    }
}
