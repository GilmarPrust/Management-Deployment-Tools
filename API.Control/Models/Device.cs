using API.Control.ValueObjects;


namespace API.Control.Models
{
    public class Device
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public required ComputerName ComputerName { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public required MacAddress MacAddress { get; init; }


        // Construtor vazio para o EF
        public Device() { }

        // Construtor com parâmetros para uso explícito
        public Device(string computerName, string serialNumber, string macAddress, DeviceModel deviceModel)
        {
            ComputerName = ComputerName.Create(computerName);
            SerialNumber = serialNumber.ToUpper();
            MacAddress = MacAddress.Create(macAddress);
            DeviceModel = deviceModel;
        }

        // DeviceModel associado ao dispositivo  
        public required Guid DeviceModelId { get; set; } = Guid.Empty;
        public required virtual DeviceModel DeviceModel { get; set; }


        // Inventory associado ao dispositivo  
        public Guid InventoryId { get; set; } = Guid.Empty;
        public virtual Inventory Inventory { get; set; } = null!;


        // Profile associado ao dispositivo  
        public Guid ProfileDeployId { get; set; } = Guid.Empty;
        public virtual ProfileDeploy ProfileDeploy { get; set; } = null!;


        // Aplicativos instalados no dispositivo  
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        // Pacotes de drivers associados ao dispositivo
        public virtual ICollection<DriverPackage> DriverPackages { get; set; } = new List<DriverPackage>();

        // Appx associado ao dispositivo  
        public virtual ICollection<AppxPackage> AppxPackages { get; set; } = new List<AppxPackage>();

    }
}
