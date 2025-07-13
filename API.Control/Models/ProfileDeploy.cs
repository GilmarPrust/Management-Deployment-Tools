using API.Control.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class ProfileDeploy
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; set; }
        public  string Description { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;

        //construtor vazio para o EF
        public ProfileDeploy() { }


        public required Guid ImageId { get; set; }
        public required virtual Image Image { get; set; }

        public virtual ICollection<string> SourcePath { get; set; } = new List<string>();

        // Aplicativos associados ao perfil.
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        // Device associado ao perfil.  
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    }
}
