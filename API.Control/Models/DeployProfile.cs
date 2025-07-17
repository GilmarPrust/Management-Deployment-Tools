using API.Control.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um perfil de implantação, incluindo imagem, aplicativos e dispositivos associados.
    /// </summary>
    public class DeployProfile
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "O nome do perfil é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do perfil deve ter no máximo 100 caracteres.")]
        public required string Name { get; set; }

        [StringLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
        public string Description { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public DeployProfile() { }

        [Required(ErrorMessage = "A imagem associada é obrigatória.")]
        public required Guid ImageId { get; set; }

        [Required(ErrorMessage = "Objeto de imagem é obrigatória.")]
        public required virtual Image Image { get; set; }

        // Se SourcePath for obrigatório, adicione [MinLength(1)]
        [MinLength(1, ErrorMessage = "Deve haver pelo menos um caminho de origem.")]
        public virtual ICollection<string> SourcePath { get; set; } = new List<string>();

        // Se pelo menos um aplicativo for obrigatório, adicione [MinLength(1)]
        [MinLength(1, ErrorMessage = "Deve haver pelo menos um aplicativo associado ao perfil.")]
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
