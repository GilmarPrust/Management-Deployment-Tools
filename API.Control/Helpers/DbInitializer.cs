namespace API.Control.Helpers
{
    public static class DbInitializer
    {
        public static void SeedDefaultData(this AppDbContext context)
        {
            // 0. Adiciona sistemas operacionais, se não existirem.
            if (!context.OperatingSystems.Any())
            {
                context.OperatingSystems.AddRange(
                    new API.Control.Entities.Auxiliary.OperatingSystem { Name = "Windows 11", ShortName = "Win11" },
                    new API.Control.Entities.Auxiliary.OperatingSystem { Name = "Windows 10", ShortName = "Win10" },
                    new API.Control.Entities.Auxiliary.OperatingSystem { Name = "Windows 8",  ShortName = "Win8" },
                    new API.Control.Entities.Auxiliary.OperatingSystem { Name = "Windows 7",  ShortName = "Win7" }
                );
                context.SaveChanges();
            }


            // 1. Adiciona fabricantes padrão, se não existirem.
            if (!context.Manufacturers.Any())
            {
                context.Manufacturers.AddRange(
                    new Manufacturer { ShortName = "Unknown", Name = "Unknown" },
                    new Manufacturer { ShortName = "Dell", Name = "Dell Inc." },
                    new Manufacturer { ShortName = "Lenovo", Name = "Lenovo" },
                    new Manufacturer { ShortName = "HP", Name = "Hewlett-Packard" },
                    new Manufacturer { ShortName = "Asus", Name = "ASUSTeK Computer Inc." }
                );
                context.SaveChanges();
            }


            // 1. Garante que o modelo de dispositivo desconhecido existe.
            if (!context.DeviceModels.Any(dm => dm.Model == "Unknown"))
            {
                var defaultmanufacturer = context.Manufacturers.First(m => m.Name == "Unknown");
                var defaultDeviceModel = new DeviceModel
                {
                    Manufacturer = defaultmanufacturer.Name,
                    Model = "Unknown",
                    Type = ""
                };
                context.DeviceModels.Add(defaultDeviceModel);
                context.SaveChanges();
            }

            // 2. Garante que o dispositivo padrão existe.
            if (!context.Devices.Any(d => d.ComputerName == new ComputerName("VM-0000")))
            {
                var device = new Device
                {
                    ComputerName = new ComputerName("VM-0000"),
                    SerialNumber = "SN000000",
                    MacAddress = new MacAddress("00-00-00-00-00-00"),
                    DeviceModelId = context.DeviceModels.First(dm => dm.Model == "Unknown").Id,
                };
                context.Devices.Add(device);
                context.SaveChanges();
            }

            // 3. Garante que o Application de teste existe.
            if (!context.Applications.Any(a => a.NameID == "TestApp"))
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
                context.Applications.Add(application);
                context.SaveChanges();
            }

            // 4. Garante que o AppxPackage de teste existe.
            if (!context.AppxPackages.Any(dm => dm.Name == "Test.AppX"))
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
                context.AppxPackages.Add(appxpackage);
                context.SaveChanges();
            }

            // 5. Garante que o DriverPack de teste existe.
            if (!context.DriverPacks.Any(dp => dp.FileName == "Test.DriverPack.cab"))
            {
                var driverPack = new DriverPack
                {
                    FileName = "Test.DriverPack.cab",
                    OS = "Win11",
                    Version = "1.0.0",
                    Source = "\\DriverPacks\\Test.DriverPack\\1.0",
                    Hash = "bbcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567891",
                    DeviceModelId = context.DeviceModels.First(dm => dm.Model == "Unknown").Id,
                    IsOEM = true,
                };
                context.DriverPacks.Add(driverPack);
                context.SaveChanges();
            }

            // 6. Garante que o Firmware de teste existe.
            if (!context.Firmwares.Any(f => f.FileName == "TestFirmware.exe"))
            {
                var firmware = new Firmware
                {
                    FileName = "TestFirmware.exe",
                    Version = "1.0.0",
                    Source = "\\Firmwares\\TestFirmware\\1.0",
                    Hash = "abcdef1234567890abcdef1234567890abcdef1234567890abcdef1234567890",
                    DeviceModelId = context.DeviceModels.First(dm => dm.Model == "Unknown").Id
                };
                context.Firmwares.Add(firmware);
                context.SaveChanges();
            }
        }
    }
}