using DCM.Core.Enums;
using DCM.Core.Entities;
using DCM.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace DCM.Core.Examples
{
    /// <summary>
    /// Exemplos de uso do sistema de tipos de dispositivo.
    /// </summary>
    public static class DeviceTypeExamples
    {
        /// <summary>
        /// Exemplo de criação de dispositivos com diferentes tipos.
        /// </summary>
        public static void CreateDeviceExamples()
        {
            // Exemplo 1: Criando um Desktop com nome automático
            var desktopDevice = new Device(
                DeviceType.Desktop,
                "ABC123456789",
                "00:11:22:33:44:55",
                Guid.NewGuid());
            // ComputerName será algo como "DSKTP-A1B2"

            // Exemplo 2: Criando um Notebook com nome específico
            var notebookDevice = new Device(
                DeviceType.Notebook,
                "XYZ987654321",
                "AA:BB:CC:DD:EE:FF",
                Guid.NewGuid());

            // Exemplo 3: Criando um Kiosk
            var kioskDevice = new Device(
                DeviceType.Kiosk,
                "KIOSK001",
                "11:22:33:44:55:66",
                Guid.NewGuid());
            // ComputerName será algo como "KIOSK-C3D4"

            // Exemplo 4: Servidor
            var serverDevice = new Device(
                DeviceType.Server,
                "SRV2024001",
                "77:88:99:AA:BB:CC",
                Guid.NewGuid());
            // ComputerName será algo como "SERVR-E5F6"
        }


        /// <summary>
        /// Demonstra o uso dos helpers de tipo de dispositivo.
        /// </summary>
        public static void DeviceTypeHelperExamples()
        {
            // Obter prefixo de um tipo
            var desktopPrefix = DeviceTypeHelper.GetPrefix(DeviceType.Desktop); // "DSKTP"
            var kioskPrefix = DeviceTypeHelper.GetPrefix(DeviceType.Kiosk);     // "KIOSK"

            // Obter tipo de um prefixo
            var typeFromPrefix = DeviceTypeHelper.GetDeviceType("NOTBK"); // DeviceType.Notebook

            // Obter descrição
            var description = DeviceTypeHelper.GetDescription(DeviceType.Server); // "Servidor"

            // Verificar se um prefixo é válido
            var isValid = DeviceTypeHelper.IsValidPrefix("DSKTP"); // true
            var isInvalid = DeviceTypeHelper.IsValidPrefix("INVALID"); // false

            // Obter todos os tipos disponíveis
            var allTypes = DeviceTypeHelper.GetAllDeviceTypes();

            // Obter todos os prefixos válidos
            var allPrefixes = DeviceTypeHelper.GetAllValidPrefixes();
        }


        /// <summary>
        /// Mostra os tipos de dispositivo disponíveis e suas descrições.
        /// </summary>
        public static Dictionary<DeviceType, string> GetAvailableDeviceTypes()
        {
            var types = new Dictionary<DeviceType, string>();

            foreach (var deviceType in DeviceTypeHelper.GetAllDeviceTypes())
            {
                types[deviceType] = DeviceTypeHelper.GetDescription(deviceType);
            }

            return types;
            
            // Resultado:
            // DeviceType.Kiosk => "Kiosk/Terminal de Autoatendimento"
            // DeviceType.Desktop => "Computador Desktop"
            // DeviceType.Notebook => "Notebook/Laptop"
            // DeviceType.Tablet => "Tablet"
            // DeviceType.Server => "Servidor"
            // DeviceType.VirtualMachine => "Máquina Virtual"
            // DeviceType.ThinClient => "Thin Client"
            // DeviceType.AllInOne => "All-in-One"
            // DeviceType.Workstation => "Estação de Trabalho"
        }
    }
}