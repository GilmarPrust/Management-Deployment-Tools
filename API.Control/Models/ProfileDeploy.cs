using API.Control.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    /// <summary>
    /// Representa um perfil de implantação, incluindo imagem, aplicativos e dispositivos associados.
    /// </summary>
    public class ProfileDeploy
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required, StringLength(100)]
        public required string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        // Construtor vazio para o EF
        public ProfileDeploy() { }

        [Required]
        public required Guid ImageId { get; set; }

        [Required]
        public required virtual Image Image { get; set; }

        public virtual ICollection<string> SourcePath { get; set; } = new List<string>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
