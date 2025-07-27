namespace API.Control.DTOs.DeployProfile
{
    public class DeployProfileUpdateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public bool Enabled { get; set; }

        [Required]
        public Guid ImageId { get; set; }

        public List<PathToCopy> PathToCopy { get; set; } = new();

        public List<Guid> ApplicationIds { get; set; } = new();

        public List<Guid> DeviceIds { get; set; } = new();

        public ProfileTaskReadDTO? ProfileTasks { get; set; } = null;
    }
}
