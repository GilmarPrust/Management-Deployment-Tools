namespace API.Control.DTOs.ProfileTask
{
    public class ProfileTaskCreateDTO
    {
        [Required] 
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public List<Guid> DeployProfileIds { get; init; } = new();
    }
}
