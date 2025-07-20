using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class DeployTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;


        public virtual DeployProfile? DeployProfile { get; set; }

    }

    public class DeployTasksList : DeployTask
    {
        public Guid ApplicationId { get; set; }
    }  
}
