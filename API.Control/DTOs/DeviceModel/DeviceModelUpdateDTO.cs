namespace API.Control.DTOs.DeviceModel
{
    /// <summary>
    /// DTO para atualização de modelo de dispositivo.
    /// </summary>
    public class DeviceModelUpdateDTO
    {
        [Required(ErrorMessage = "O campo Manufacturer é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Manufacturer deve ter no máximo 100 caracteres.")]
        public string Manufacturer { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Model é obrigatório.")]
        [StringLength(100, ErrorMessage = "O campo Model deve ter no máximo 100 caracteres.")]
        public string Model { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "O campo Type deve ter no máximo 50 caracteres.")]
        public string Type { get; set; } = string.Empty;


    }
}
