using System.ComponentModel;

namespace DCM.Core.Enums
{
    /// <summary>
    /// Enumera os tipos de dispositivos suportados no sistema.
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// Kiosk ou Terminal de Autoatendimento.
        /// </summary>
        [Description("Kiosk/Terminal de Autoatendimento")]
        Kiosk = 1,

        /// <summary>
        /// Computador Desktop.
        /// </summary>
        [Description("Computador Desktop")]
        Desktop = 2,

        /// <summary>
        /// Notebook ou Laptop.
        /// </summary>
        [Description("Notebook/Laptop")]
        Notebook = 3,

        /// <summary>
        /// Tablet.
        /// </summary>
        [Description("Tablet")]
        Tablet = 4,

        /// <summary>
        /// Servidor.
        /// </summary>
        [Description("Servidor")]
        Server = 5,

        /// <summary>
        /// Máquina Virtual.
        /// </summary>
        [Description("Máquina Virtual")]
        VirtualMachine = 6,

        /// <summary>
        /// Thin Client.
        /// </summary>
        [Description("Thin Client")]
        ThinClient = 7,

        /// <summary>
        /// All-in-One.
        /// </summary>
        [Description("All-in-One")]
        AllInOne = 8,

        /// <summary>
        /// Estação de Trabalho.
        /// </summary>
        [Description("Estação de Trabalho")]
        Workstation = 9
    }
}