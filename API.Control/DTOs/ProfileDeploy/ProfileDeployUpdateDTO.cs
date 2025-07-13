using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.ProfileDeploy
{
    public class ProfileDeployUpdateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public bool Enabled { get; set; }

        [Required]
        public Guid ImageId { get; set; }

        public List<String> SourcePath { get; set; } = new();

        public List<Guid> ApplicationIds { get; set; } = new();

        public List<Guid> DeviceIds { get; set; } = new();
    }
}
