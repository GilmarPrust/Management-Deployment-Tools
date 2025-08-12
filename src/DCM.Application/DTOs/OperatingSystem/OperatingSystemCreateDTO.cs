using System.ComponentModel.DataAnnotations;

namespace DCM.Application.DTOs.OperatingSystem
{
    /// <summary>
    /// DTO para criação de sistema operacional.
    /// </summary>
    public class OperatingSystemCreateDTO
    {
        /// <summary>
        /// Nome do sistema operacional.
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Nome curto do sistema operacional.
        /// </summary>
        [Required, StringLength(5)]
        public string ShortName { get; init; } = string.Empty;
    }
}
