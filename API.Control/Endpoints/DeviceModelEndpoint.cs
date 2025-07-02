using System.ComponentModel.DataAnnotations;



namespace API.Control.Endpoints
{
    public class DeviceModelEndpoint
    {
        public Guid Guid { get; set; }


        [Required(ErrorMessage = "Fabricante é obrigatório.")]
        public string Manufacturer { get; set; } = string.Empty;


        [Required(ErrorMessage = "Modelo é obrigatório.")]
        public string Model { get; set; } = string.Empty;


        public string Type { get; set; } = string.Empty;
    }
}