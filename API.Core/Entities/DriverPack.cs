using System;
using System.ComponentModel.DataAnnotations;

namespace DCM.Core.Entities
{
    /// <summary>
    /// Representa um pacote de driver associado a um modelo de dispositivo.
    /// </summary>
    public class DriverPack : BaseEntity
    {
        /// <summary>
        /// Nome do arquivo do pacote de driver.
        /// </summary>
        [Required, StringLength(100)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Sistema operacional suportado pelo pacote de driver.
        /// </summary>
        [Required, StringLength(50)]
        public string OS { get; set; } = string.Empty;

        /// <summary>
        /// Versão do pacote de driver.
        /// </summary>
        [Required, StringLength(50)]
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Caminho de origem do pacote de driver.
        /// </summary>
        [Required, StringLength(250)]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// Hash do pacote de driver.
        /// </summary>
        [Required, StringLength(64)]
        public string Hash { get; set; } = string.Empty;

        /// <summary>
        /// ID do modelo de dispositivo ao qual o pacote de driver está vinculado (opcional para não-OEM).
        /// </summary>
        public Guid? DeviceModelId { get; set; }

        /// <summary>
        /// Modelo de dispositivo associado ao pacote de driver (opcional para não-OEM).
        /// </summary>
        public virtual DeviceModel DeviceModel { get; set; }

        /// <summary>
        /// Indica se o pacote de driver é OEM.
        /// </summary>
        [Required]
        public bool IsOEM { get; set; }

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public DriverPack() { }
    }
}
