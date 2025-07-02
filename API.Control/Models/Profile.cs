using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class Profile
    {
        public Guid Guid { get; set; } = Guid.NewGuid();


        [Required(ErrorMessage = "Nome do computador é obrigatório.")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Descrição é obrigatória.")]
        public string Description { get; set; } = string.Empty;


        [Required(ErrorMessage = "Guid da imagem é obrigatória.")]
        public string ImageGuid { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "Guid do aplicativo é obrigatória.")]
        public string[] Applications { get; set; } = Array.Empty<string>();


        [Required(ErrorMessage = "Pastas para copiar OEM.")]
        public string SourcePath { get; set; } = string.Empty;

    }
}
