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

        public DeployProfile() { }

        [Required(ErrorMessage = "A imagem associada é obrigatória.")]
        public required Guid ImageId { get; set; }
        public virtual Image Image { get; set; } = null!;


        public virtual ICollection<String> SourcePath { get; set; } = new List<String>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
        public virtual ICollection<ProfileTask> DeployTasks { get; set; } = new List<ProfileTask>();
    }
}
