using DCM.Core.Entities;
using DCM.Core.Utilities;

namespace DCM.Infrastructure.Persistence
{
    public static class DbInitializerExtensions
    {
        public static void SeedDefaultData(this AppDbContext context)
        {
            var initializer = new DbInitializer(context);
            initializer.Initialize();
        }
    }

    public class DbInitializer(AppDbContext context) : IDbInitializer
    {
        private readonly AppDbContext _context = context;

        public void Initialize()
        {
            // 0. Adiciona sistemas operacionais, se não existirem.
            if (!_context.OperatingSystems.Any())
            {
                _context.OperatingSystems.AddRange(
                    new DCM.Core.Entities.OperatingSystem { Name = "Windows 11", ShortName = "Win11" },
                    new DCM.Core.Entities.OperatingSystem { Name = "Windows 10", ShortName = "Win10" },
                    new DCM.Core.Entities.OperatingSystem { Name = "Windows 8", ShortName = "Win8" },
                    new DCM.Core.Entities.OperatingSystem { Name = "Windows 7", ShortName = "Win7" }
                );
                _context.SaveChanges();
            }

            // 1. Adiciona fabricantes padrão, se não existirem.
            if (!_context.Manufacturers.Any())
            {
                _context.Manufacturers.AddRange(
                    new Manufacturer { ShortName = "Unknown", Name = "Unknown" },
                    new Manufacturer { ShortName = "Dell", Name = "Dell Inc." },
                    new Manufacturer { ShortName = "Lenovo", Name = "Lenovo" },
                    new Manufacturer { ShortName = "HP", Name = "Hewlett-Packard" },
                    new Manufacturer { ShortName = "Asus", Name = "ASUSTeK Computer Inc." }
                );
                _context.SaveChanges();
            }

            // 2. Garante que o modelo de dispositivo desconhecido existe.
            if (!_context.DeviceModels.Any(dm => dm.Model == "Unknown"))
            {
                var defaultmanufacturer = _context.Manufacturers.First(m => m.Name == "Unknown");
                var defaultDeviceModel = new DeviceModel
                {
                    Manufacturer = defaultmanufacturer.Name,
                    Model = "Unknown",
                    Type = ""
                };
                _context.DeviceModels.Add(defaultDeviceModel);
                _context.SaveChanges();

            }

            // 3. Garante que o dispositivo padrão existe.
            if (!_context.Devices.Any(d => d.ComputerName == new ComputerName("VM-0000")))
            {
                var device = new Device
                {
                    ComputerName = new ComputerName("VM-0000"),
                    SerialNumber = "SN000000",
                    MacAddress = new MacAddress("00-00-00-00-00-00"),
                    DeviceModelId = _context.DeviceModels.First(dm => dm.Model == "Unknown").Id,
                };
                _context.Devices.Add(device);
                _context.SaveChanges();
            }

            // 4. Garante que o Application de teste existe.
            if (!_context.Applications.Any(a => a.NameID == "TestApp"))
            {
                var application = new Application
                {
                    NameID = "TestApp",
                    DisplayName = "Aplicativo para testes",
                    Version = "1.0",
                    FileName = "SetupTestApp.exe",
                    Argument = "--quiet --force",
                    Source = "\\Applications\\TestApp\\1.0",
                    Filter = "",
                    Hash = "1234567890abcdef1234567890abcdef1234567890abcdef1234567890fdsaer",
                };
                _context.Applications.Add(application);
                _context.SaveChanges();
            }

            // 5. Garante que o AppxPackage de teste existe.
            if (!_context.AppxPackages.Any(dm => dm.Name == "Test.AppX"))
            {
                var appxpackage = new AppxPackage
                {
                    Name = "Test.AppX",
                    Version = "1.0",
                    Publisher = "CN=Microsoft Corporation, O=Microsoft Corporation, L=Redmond, S=Washington, C=US",
                    Architecture = "X64",
                    PackageFamilyName = "Microsoft.AppxForWindows_8wekyb3d8bbwe",
                    PackageFullName = "Microsoft.AppxForWindows_1.0000.000.000_x64__8wekyb3d8bbwe",
                    Status = "OK"
                };
                _context.AppxPackages.Add(appxpackage);
                _context.SaveChanges();
            }

            // 6. Garante que o DriverPack de teste existe.
            if (!_context.DriverPacks.Any(dp => dp.FileName == "Test.DriverPack.cab"))
            {
                var driverPack = new DriverPack
                {
                    FileName = "Test.DriverPack.cab",
                    OS = "Win11",
                    Version = "1.0.0",
                    Source = "\\DriverPacks\\Test.DriverPack\\1.0",
                    Hash = "bbcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567891",
                    DeviceModelId = _context.DeviceModels.First(dm => dm.Model == "Unknown").Id,
                    IsOEM = true,
                };
                _context.DriverPacks.Add(driverPack);
                _context.SaveChanges();
            }

            // 7. Garante que o Firmware de teste existe.
            if (!_context.Firmwares.Any(f => f.FileName == "TestFirmware.exe"))
            {
                var firmware = new Firmware
                {
                    FileName = "TestFirmware.exe",
                    Version = "1.0.0",
                    Source = "\\Firmwares\\TestFirmware\\1.0",
                    Hash = "abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890",
                    DeviceModelId = _context.DeviceModels.First(dm => dm.Model == "Unknown").Id
                };
                _context.Firmwares.Add(firmware);
                _context.SaveChanges();

                //update Enabled to false.
                firmware.Enabled = false;
                _context.Firmwares.Update(firmware);
                _context.SaveChanges();
            }
        }
    }
}