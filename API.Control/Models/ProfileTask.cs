using System.ComponentModel.DataAnnotations;

namespace API.Control.Models
{
    public class ProfileTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public required Guid ImageId { get; set; }
        public virtual ICollection<DeployProfile> DeployProfiles { get; set; } = new List<DeployProfile>();

    }

    public class ProfileTaskList : ProfileTask
    {
        public Guid ApplicationId { get; set; }
    }  
}
