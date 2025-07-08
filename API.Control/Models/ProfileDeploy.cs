using API.Control.ValueObjects;

namespace API.Control.Models
{
    public class ProfileDeploy
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; set; }
        public  string Description { get; set; } = string.Empty;
        public required Image Image { get; set; }

        //construtor vazio para o EF
        public ProfileDeploy() { }

        // Construtor com parâmetros para uso explícito
        public ProfileDeploy(string name, string description, Image image)
        {
            Name = name;
            Description = description;
            Image = image;
        }

        public virtual ICollection<string> SourcePath { get; set; } = new List<string>();


        // Dispositivos associados ao perfil.
        public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

        // Aplicativos associados ao perfil.
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

        // Tarefas associadas ao perfil.
        public virtual ICollection<Task> ProfileTasks { get; set; } = new List<Task>();
    }
}
