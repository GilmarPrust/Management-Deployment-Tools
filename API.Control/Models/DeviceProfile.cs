using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DeviceProfile
    {
        [Required(ErrorMessage = "Guid do profile é obrigatório.")]
        public string ProfileGuid { get; set; } = string.Empty;

        [Required(ErrorMessage = "Guid do Device é obrigatório.")]
        public string[] DeviceGuid { get; set; } = Array.Empty<string>();


    }
}
