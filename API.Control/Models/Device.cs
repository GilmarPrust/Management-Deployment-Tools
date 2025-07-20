using API.Control.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um dispositivo físico, incluindo informações de identificação e associações.
    /// </summary>
    public class Device
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nome do computador é obrigatório")]
        public required ComputerName ComputerName { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Número de série deve ter entre 5 e 100 caracteres")]
        [RegularExpression(@"^[A-Z0-9\-]+$", ErrorMessage = "Número de série deve conter apenas letras maiúsculas, números e hífens")]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Endereço MAC é obrigatório")]
        public required MacAddress MacAddress { get; init; }
        
        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public Device() { }

        [Required(ErrorMessage = "Modelo de dispositivo é obrigatório")]
        public required Guid DeviceModelId { get; set; }
        public virtual DeviceModel DeviceModel { get; set; } = null!;

        // Inventário associado ao dispositivo.
        public virtual Inventory? Inventory { get; set; } = null;

        // Perfil de implantação associado ao dispositivo.
        public virtual DeployProfile? DeployProfile { get; set; } = null;

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();
        public virtual ICollection<AppxPackage> AppxPackages { get; set; } = new List<AppxPackage>();
    }
}
