namespace API.Control.Entities
{
    /// <summary>
    /// Representa um dispositivo físico, incluindo informações de identificação e associações.
    /// </summary>
    public class Device : BaseEntity
    {
        /// <summary>
        /// Nome do computador do dispositivo.
        /// </summary>
        [Required]
        public required ComputerName ComputerName { get; set; }

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
        public required MacAddress MacAddress { get; init; }

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public Device() { }

        /// <summary>
        /// ID do modelo de dispositivo.
        /// </summary>
        [Required]
        public required Guid DeviceModelId { get; set; }

        /// <summary>
        /// Modelo do dispositivo.
        /// </summary>
        public virtual DeviceModel DeviceModel { get; set; } = null!;

        /// <summary>
        /// Inventário associado ao dispositivo.
        /// </summary>
        public virtual Inventory? Inventory { get; set; } = null;

        /// <summary>
        /// Perfil de implantação associado ao dispositivo.
        /// </summary>
        public virtual DeployProfile? DeployProfile { get; set; } = null;

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
    }
}
