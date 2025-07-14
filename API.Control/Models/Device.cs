using API.Control.ValueObjects;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.Control.Models
{
    public class Device
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public required ComputerName ComputerName { get; set; }

        [Required, StringLength(100)]
        public string SerialNumber { get; set; } = string.Empty;
        public required MacAddress MacAddress { get; init; }
        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public Device() { }


        // DeviceModel associado ao dispositivo  
        public required Guid DeviceModelId { get; set; }
        public required virtual DeviceModel DeviceModel { get; set; }

        // Inventory associado ao dispositivo  
        public virtual Inventory? Inventory { get; set; }


        // Profile associado ao dispositivo  
        public virtual ProfileDeploy? ProfileDeploy { get; set; }

        // Aplicativos associado ao dispositivo  
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        // Pacotes de drivers associados ao dispositivo
        public virtual ICollection<DriverPack> DriverPacks { get; set; } = new List<DriverPack>();

        // Appx associado ao dispositivo  
        public virtual ICollection<AppxPackage> AppxPackages { get; set; } = new List<AppxPackage>();
    }
}
