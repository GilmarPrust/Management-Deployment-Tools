namespace API.Control.Entities
{
    /// <summary>
    /// Representa um dispositivo físico, incluindo informações de identificação e associações.
    /// </summary>
    public class Device : _BaseEntity
    {
        [Required]
        public required ComputerName ComputerName { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z0-9\-]+$", ErrorMessage = "Número de série deve conter apenas letras maiúsculas, números e hífens")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        public required MacAddress MacAddress { get; init; }
        

        // Construtor vazio para o EF
        public Device() { }


        [Required]
        public required Guid DeviceModelId { get; set; }
        public virtual DeviceModel DeviceModel { get; set; } = null!;
        public virtual Inventory? Inventory { get; set; } = null;
        public virtual DeployProfile? DeployProfile { get; set; } = null;
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();
        public virtual ICollection<AppxPackage> AppxPackages { get; set; } = new List<AppxPackage>();
    }
}
