using DCM.Application.DTOs.DeviceModel;
using DCM.Application.DTOs.Inventory;

namespace DCM.Application.DTOs.Device
{
    /// <summary>
    /// DTO para leitura de dispositivo.
    /// </summary>
    public class DeviceReadDTO
    {
        /// <summary>
        /// Identificador único do dispositivo.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome do computador do dispositivo.
        /// </summary>
        public string ComputerName { get; init; } = string.Empty;

        /// <summary>
        /// Número de série do dispositivo.
        /// </summary>
        public string SerialNumber { get; init; } = string.Empty;

        /// <summary>
        /// Endereço MAC do dispositivo.
        /// </summary>
        public string MacAddress { get; init; } = string.Empty;

        /// <summary>
        /// Indica se o dispositivo está habilitado.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Modelo do dispositivo.
        /// </summary>
        public DeviceModelReadDTO DeviceModel { get; init; } = null!;

        /// <summary>
        /// Inventário associado ao dispositivo.
        /// </summary>
        public InventoryReadDTO? Inventory { get; init; } = null;

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
