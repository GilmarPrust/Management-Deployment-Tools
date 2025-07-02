using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class BlackListAppx
    {
        [Required(ErrorMessage = "Nome do sistema operacional é obrigatório.")]
        public string OS { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nome do pacote é obrigatório.")]
        public string[] Packages { get; set; } = Array.Empty<string>();
    }
}
