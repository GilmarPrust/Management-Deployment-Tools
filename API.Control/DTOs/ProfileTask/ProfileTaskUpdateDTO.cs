using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.ProfileTask
{
    public class ProfileTaskUpdateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public bool Enabled { get; set; }

    }
}
