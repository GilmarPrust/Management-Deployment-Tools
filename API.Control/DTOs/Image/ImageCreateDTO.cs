namespace API.Control.DTOs.Image
{
    public class ImageCreateDTO
    {
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
        public long ImageSize { get; set; } = 0;

        [Required] 
        public string Source { get; set; } = string.Empty;

    }
}
