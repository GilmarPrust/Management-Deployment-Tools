using System.ComponentModel.DataAnnotations;

namespace API.Control2.DTOs
{
    public class ImagenDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string ImageName { get; set; } = string.Empty;

        [Required]
        public string ImageDescription { get; set; } = string.Empty;

        [Required]
        public string ImageIndex { get; set; } = string.Empty;

        [Required]
        public string ShortName { get; set; } = string.Empty;

        [Required]
        public string EditionId { get; set; } = string.Empty;

        [Required]
        public string Version { get; set; } = string.Empty;

        [Required]
        public string[] Languages { get; set; } = Array.Empty<string>();

        [Required]
        public string ImageSize { get; set; } = string.Empty;

        [Required]
        public string Source { get; set; } = string.Empty;
    }
}
