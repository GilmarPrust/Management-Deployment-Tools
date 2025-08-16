using DCM.Core.ValueObjects;
using DCM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DCM.Core.Entities
{
    /// <summary>
    /// Representa um dispositivo físico, incluindo informações de identificação e associações.
    /// </summary>
    public class Device : BaseEntity
    {
        /// <summary>
        /// Tipo do dispositivo (ex: Desktop, Notebook, Server, etc.).
        /// </summary>
        [Required]
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// Nome do computador do dispositivo.
        /// </summary>
        [Required]
        public ComputerName ComputerName { get; set; }

        /// <summary>
        /// Número de série do dispositivo.
        /// </summary>
        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z0-9\-]+$", ErrorMessage = "Número de série deve conter apenas letras maiúsculas, números e hífens")]
        public string SerialNumber { get; set; } = string.Empty;

        /// <summary>
        /// Endereço MAC do dispositivo.
        /// </summary>
        [Required]
        public MacAddress MacAddress { get; set; }

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public Device() { }

        /// <summary>
        /// Construtor para criação de dispositivo com tipo específico.
        /// </summary>
        /// <param name="deviceType">Tipo do dispositivo</param>
        /// <param name="serialNumber">Número de série</param>
        /// <param name="macAddress">Endereço MAC</param>
        /// <param name="deviceModelId">ID do modelo de dispositivo</param>
        /// <param name="computerName">Nome do computador (opcional - será gerado se não fornecido)</param>
        public Device(DeviceType deviceType, string serialNumber, string macAddress, Guid deviceModelId)
        {
            DeviceType = deviceType;
            SerialNumber = serialNumber?.ToUpperInvariant() ?? string.Empty;
            MacAddress = new MacAddress(macAddress);
            ComputerName = new ComputerName(deviceType);
            DeviceModelId = deviceModelId;
        }

        /// <summary>
        /// ID do modelo de dispositivo.
        /// </summary>
        [Required]
        public Guid DeviceModelId { get; set; }

        /// <summary>
        /// Modelo do dispositivo.
        /// </summary>
        public virtual DeviceModel DeviceModel { get; set; }

        /// <summary>
        /// ID do perfil de implantação associado ao dispositivo (opcional).
        /// </summary>
        public Guid? DeployProfileId { get; set; }

        /// <summary>
        /// Perfil de implantação associado ao dispositivo.
        /// </summary>
        public virtual DeployProfile? DeployProfile { get; set; }

        /// <summary>
        /// Inventário associado ao dispositivo.
        /// </summary>
        public virtual Inventory? Inventory { get; set; } = null;

        /// <summary>
        /// Aplicativos associados ao dispositivo.
        /// </summary>
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        /// <summary>
        /// Pacotes de driver associados ao dispositivo.
        /// </summary>
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();

        /// <summary>
        /// Pacotes Appx associados ao dispositivo.
        /// </summary>
        public virtual ICollection<AppxPackage> AppxPackages { get; set; } = new List<AppxPackage>();

        /// <summary>
        /// Obtém a descrição do tipo de dispositivo.
        /// </summary>
        /// <returns>Descrição do tipo de dispositivo</returns>
        public string GetDeviceTypeDescription()
        {
            return DeviceTypeHelper.GetDescription(DeviceType);
        }
    }
}
