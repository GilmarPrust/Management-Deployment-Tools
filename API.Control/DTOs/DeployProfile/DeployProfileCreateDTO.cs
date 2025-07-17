using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.ProfileDeploy
{
    public class DeployProfileCreateDTO
    {
        [Required] 
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required] 
        public Guid ImageId { get; set; }
    }
}
