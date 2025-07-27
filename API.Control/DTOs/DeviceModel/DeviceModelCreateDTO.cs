namespace API.Control.DTOs.DeviceModel
{
    public class DeviceModelCreateDTO
    {
        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer { get; set; } = string.Empty;


        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}
