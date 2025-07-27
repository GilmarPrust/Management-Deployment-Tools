namespace API.Control.DTOs.Manufacturer
{
    public class ManufacturerCreateDTO
    {

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string ShortName { get; set; } = string.Empty;

    }
}
