using System.ComponentModel.DataAnnotations;

namespace API.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para criação de modelo de dispositivo.
    /// </summary>
    public class DeviceModelCreateDTO
    {
        /// <summary>
        /// Nome do fabricante do modelo de dispositivo.
        /// </summary>
        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer { get; init; } = string.Empty;

        /// <summary>
        /// Nome do modelo do dispositivo.
        /// </summary>
        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; init; } = string.Empty;

        /// <summary>
        /// Tipo do modelo de dispositivo.
        /// </summary>
        public string Type { get; init; } = string.Empty;
    }
}
