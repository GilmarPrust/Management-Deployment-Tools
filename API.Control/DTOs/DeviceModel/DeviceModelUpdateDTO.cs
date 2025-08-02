namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para atualização de modelo de dispositivo.
    /// </summary>
    public class DeviceModelUpdateDTO
    {
        /// <summary>
        /// Nome do fabricante do modelo de dispositivo.
        /// </summary>
        [Required(ErrorMessage = "O campo Manufacturer é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Manufacturer deve ter no máximo 100 caracteres.")]
        public string Manufacturer { get; init; } = string.Empty;

        /// <summary>
        /// Nome do modelo do dispositivo.
        /// </summary>
        [Required(ErrorMessage = "O campo Model é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Model deve ter no máximo 100 caracteres.")]
        public string Model { get; init; } = string.Empty;

        /// <summary>
        /// Tipo do modelo de dispositivo.
        /// </summary>
        [StringLength(50, ErrorMessage = "O campo Type deve ter no máximo 50 caracteres.")]
        public string Type { get; init; } = string.Empty;
    }
}
