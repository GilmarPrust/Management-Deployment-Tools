using System.ComponentModel.DataAnnotations;

namespace API.Control.DTOs.Application
{
    public class ApplicationCreateDTO
    {
        [Required] 
        public string NameID { get; set; } = string.Empty;

        [Required] 
        public string DisplayName { get; set; } = string.Empty;

        [Required] 
        public string Version { get; set; } = string.Empty;

        [Required] 
        public string FileName { get; set; } = string.Empty;

        public string Argument { get; set; } = string.Empty;

        [Required] 
        public string Source { get; set; } = string.Empty;

        public string Filter { get; set; } = string.Empty;

        [Required]
        public string Hash { get; set; } = string.Empty;

    }
}
