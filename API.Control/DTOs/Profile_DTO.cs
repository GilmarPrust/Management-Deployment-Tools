using System.ComponentModel.DataAnnotations;

namespace API.Control2.DTOs
{
    public class Profile_DTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public Guid ImageId { get; set; }
    }
}
