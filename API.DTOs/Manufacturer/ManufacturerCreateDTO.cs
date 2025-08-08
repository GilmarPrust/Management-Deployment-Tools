using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Manufacturer
{
    /// <summary>
    /// DTO para criação de fabricante.
    /// </summary>
    public class ManufacturerCreateDTO
    {
        /// <summary>
        /// Nome completo do fabricante.
        /// </summary>
        [Required, StringLength(50)]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Nome curto do fabricante.
        /// </summary>
        [Required, StringLength(5)]
        public string ShortName { get; init; } = string.Empty;
    }
}
