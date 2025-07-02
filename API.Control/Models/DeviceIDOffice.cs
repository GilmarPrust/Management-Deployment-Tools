using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DeviceIDOffice
    {
        [Required(ErrorMessage = "Guid do Device é obrigatório.")]
        public string DeviceGuid { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product ID do office é obrigatório.")]
        public string PIDOffice { get; set; } = string.Empty;
    }
}
