using System;
using System.ComponentModel;
using System.Reflection;
using DCM.Core.Enums;

namespace DCM.Core.ValueObjects
{
    /// <summary>
    /// Helper para trabalhar com tipos de dispositivo e seus prefixos.
    /// </summary>
    public static class DeviceTypeHelper
    {
        /// <summary>
        /// Obtém o prefixo de computador baseado no tipo de dispositivo.
        /// </summary>
        /// <param name="deviceType">Tipo do dispositivo</param>
        /// <returns>Prefixo correspondente ao tipo</returns>
        public static string GetPrefix(DeviceType deviceType)
        {
            return deviceType switch
            {
                DeviceType.Kiosk => "KIOSK",
                DeviceType.Desktop => "DSKTP",
                DeviceType.Notebook => "NOTBK",
                DeviceType.Tablet => "TABLT",
                DeviceType.Server => "SERVR",
                DeviceType.VirtualMachine => "VM",
                DeviceType.ThinClient => "THIN",
                DeviceType.AllInOne => "AIO",
                DeviceType.Workstation => "WORK",
                _ => "UNKNW"
            };
        }

        /// <summary>
        /// Obtém o tipo de dispositivo baseado no prefixo.
        /// </summary>
        /// <param name="prefix">Prefixo do computador</param>
        /// <returns>Tipo de dispositivo correspondente</returns>
        public static DeviceType GetDeviceType(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Prefixo não pode ser vazio ou nulo.", nameof(prefix));

            var normalizedPrefix = prefix.Trim().ToUpperInvariant();

            return normalizedPrefix switch
            {
                "KIOSK" => DeviceType.Kiosk,
                "DSKTP" => DeviceType.Desktop,
                "NOTBK" => DeviceType.Notebook,
                "TABLT" => DeviceType.Tablet,
                "SERVR" => DeviceType.Server,
                "VM" => DeviceType.VirtualMachine,
                "THIN" => DeviceType.ThinClient,
                "AIO" => DeviceType.AllInOne,
                "WORK" => DeviceType.Workstation,
                _ => throw new ArgumentException($"Prefixo '{prefix}' não é válido.", nameof(prefix))
            };
        }

        /// <summary>
        /// Obtém a descrição do tipo de dispositivo.
        /// </summary>
        /// <param name="deviceType">Tipo do dispositivo</param>
        /// <returns>Descrição do tipo</returns>
        public static string GetDescription(DeviceType deviceType)
        {
            var field = deviceType.GetType().GetField(deviceType.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? deviceType.ToString();
        }

        /// <summary>
        /// Verifica se um prefixo é válido.
        /// </summary>
        /// <param name="prefix">Prefixo a ser verificado</param>
        /// <returns>True se o prefixo for válido, false caso contrário</returns>
        public static bool IsValidPrefix(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                return false;

            try
            {
                GetDeviceType(prefix);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtém todos os tipos de dispositivo disponíveis.
        /// </summary>
        /// <returns>Array com todos os tipos de dispositivo</returns>
        public static DeviceType[] GetAllDeviceTypes()
        {
            return Enum.GetValues<DeviceType>();
        }

        /// <summary>
        /// Obtém todos os prefixos válidos.
        /// </summary>
        /// <returns>Array com todos os prefixos válidos</returns>
        public static string[] GetAllValidPrefixes()
        {
            var deviceTypes = GetAllDeviceTypes();
            var prefixes = new string[deviceTypes.Length];
            
            for (int i = 0; i < deviceTypes.Length; i++)
            {
                prefixes[i] = GetPrefix(deviceTypes[i]);
            }
            
            return prefixes;
        }
    }
}