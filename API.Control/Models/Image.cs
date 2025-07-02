namespace API.Control.Models
{
    public class Image
    {

        public Guid Guid { get; set; } = Guid.NewGuid();
        public string ImageName { get; set; } = string.Empty;
        public string ImageDescription { get; set; } = string.Empty;
        public string ImageIndex { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string EditionId { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string[] Languages { get; set; } = Array.Empty<string>();
        public string ImageSize { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;

    }
}
