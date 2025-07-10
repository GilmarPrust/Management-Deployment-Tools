using API.Control.ValueObjects;
using AutoMapper;


namespace API.Control.Models
{
    public class Device
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public ComputerName ComputerName { get; set; } = null!;
        public string SerialNumber { get; set; } = string.Empty;
        public MacAddress MacAddress { get; init; } = null!;

        // Construtor vazio para o EF
        public Device() { }

        public Device(string computerName, string serialNumber, string macAddress, DeviceModel deviceModel)
        {
            if (string.IsNullOrWhiteSpace(computerName))
                throw new ArgumentException("Computer name is required.", nameof(computerName));

            if (string.IsNullOrWhiteSpace(macAddress))
                throw new ArgumentException("MAC Address is required.", nameof(macAddress));

            if (deviceModel == null)
                throw new ArgumentException("Device model is required.", nameof(deviceModel));

            SerialNumber = serialNumber.ToUpper();
            MacAddress = MacAddress.Create(macAddress);
            DeviceModel = deviceModel;
            ComputerName = ComputerName.Create(computerName);
        }

        // DeviceModel associado ao dispositivo  
        public required Guid DeviceModelId { get; set; } = Guid.Empty;
        public required virtual DeviceModel DeviceModel { get; set; }

        // Inventory associado ao dispositivo  
        public virtual Inventory Inventory { get; set; } = null!;


        // Profile associado ao dispositivo  
        public required Guid ProfileDeployId { get; set; } = Guid.Empty;
        public virtual ProfileDeploy ProfileDeploy { get; set; } = null!;

        // Aplicativos associado ao dispositivo  
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        // Pacotes de drivers associados ao dispositivo
        public virtual ICollection<DriverPackage> DriverPackages { get; set; } = new List<DriverPackage>();

        // Appx associado ao dispositivo  
        public virtual ICollection<AppxPackage> AppxPackages { get; set; } = new List<AppxPackage>();
    }
}
