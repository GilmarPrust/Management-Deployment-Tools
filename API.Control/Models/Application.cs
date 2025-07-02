using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class Application
    {
        
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O NameID é obrigatório.")]
        public string NameID { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O nome de exibição é obrigatório.")]
        public string DisplayName { get; set; } = string.Empty;

        [Required(ErrorMessage = "A versão é obrigatória.")]
        public string Version { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome do arquivo é obrigatório.")]
        public string FileName { get; set; } = string.Empty;

        public string? Argument { get; set; } = null;

        [Required(ErrorMessage = "O Caminho é obrigatório.")]
        public string Source { get; set; } = string.Empty;

        public string? Hash { get; set; } = null;
        public string? Filter { get; set; } = null;
        
        public bool Enabled { get; set; } = true;
    }
}
