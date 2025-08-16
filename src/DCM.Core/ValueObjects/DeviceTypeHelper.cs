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
        /// Obt�m o prefixo de computador baseado no tipo de dispositivo.
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
        /// Obt�m o tipo de dispositivo baseado no prefixo.
        /// </summary>
        /// <param name="prefix">Prefixo do computador</param>
        /// <returns>Tipo de dispositivo correspondente</returns>
        public static DeviceType GetDeviceType(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("Prefixo n�o pode ser vazio ou nulo.", nameof(prefix));

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
                _ => throw new ArgumentException($"Prefixo '{prefix}' n�o � v�lido.", nameof(prefix))
            };
        }

        /// <summary>
        /// Obt�m a descri��o do tipo de dispositivo.
        /// </summary>
        /// <param name="deviceType">Tipo do dispositivo</param>
        /// <returns>Descri��o do tipo</returns>
        public static string GetDescription(DeviceType deviceType)
        {
            var field = deviceType.GetType().GetField(deviceType.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? deviceType.ToString();
        }

        /// <summary>
        /// Verifica se um prefixo � v�lido.
        /// </summary>
        /// <param name="prefix">Prefixo a ser verificado</param>
        /// <returns>True se o prefixo for v�lido, false caso contr�rio</returns>
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
        /// Obt�m todos os tipos de dispositivo dispon�veis.
        /// </summary>
        /// <returns>Array com todos os tipos de dispositivo</returns>
        public static DeviceType[] GetAllDeviceTypes()
        {
            return Enum.GetValues<DeviceType>();
        }

        /// <summary>
        /// Obt�m todos os prefixos v�lidos.
        /// </summary>
        /// <returns>Array com todos os prefixos v�lidos</returns>
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