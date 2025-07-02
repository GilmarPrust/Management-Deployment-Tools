using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DeviceApplication
    {
        [Required(ErrorMessage = "Guid do Device é obrigatório.")]
        public string DeviceGuid { get; set; } = string.Empty;

        [Required(ErrorMessage = "Guid do aplicativo é obrigatório.")]
        public string[] Applications { get; set; } = Array.Empty<string>();
    }
}
