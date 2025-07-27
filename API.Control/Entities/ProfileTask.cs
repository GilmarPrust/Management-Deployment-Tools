namespace API.Control.Entities
{
    public class ProfileTask : _BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ProfileTask() { }

        public virtual ICollection<DeployProfile> DeployProfiles { get; set; } = new List<DeployProfile>();

    }  
}
