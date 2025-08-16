using System;
using System.ComponentModel.DataAnnotations;

namespace DCM.Core.Entities
{
    /// <summary>
    /// Representa um sistema operacional, incluindo nome, versão, arquitetura e identificadores.
    /// </summary>
    public class OperatingSystem
    {
        /// <summary>
        /// Identificador único do sistema operacional.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome do sistema operacional.
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Nome curto do sistema operacional.
        /// </summary>
        [Required, StringLength(5)]
        public string ShortName { get; set; } = string.Empty;

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public OperatingSystem() { }
    }
}
