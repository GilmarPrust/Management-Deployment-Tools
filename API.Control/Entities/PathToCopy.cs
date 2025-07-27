namespace API.Control.Entities
{
    public class PathToCopy : _BaseEntity
    {
        [Required]
        public required Guid DeployProfileId { get; set; }

        [Required, StringLength(250)]
        public string Path { get; set; } = string.Empty;


        public PathToCopy() { }

        public virtual ProfileTask ProfileTasks { get; set; } = new();
    }
}
