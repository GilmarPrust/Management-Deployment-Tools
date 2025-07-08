namespace API.Control.Models
{
    public class Image
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string ImageName { get; set; }
        public required string ImageDescription { get; set; }
        public required string ImageIndex { get; set; }
        public required string ShortName { get; set; }
        public required string EditionId { get; set; }
        public required string Version { get; set; }
        public required string[] Languages { get; set; }
        public required long ImageSize { get; set; }
        public required string Source { get; set; }

        // Contrutor vazio para o EF
        public Image() { }

        // Construtor com parâmetros para uso explícito
        public Image(string imageName, string imageDescription, string imageIndex, string shortName, string editionId, string version, string[] languages, long imageSize, string source)
        {
            ImageName = imageName;
            ImageDescription = imageDescription;
            ImageIndex = imageIndex;
            ShortName = shortName;
            EditionId = editionId;
            Version = version;
            Languages = languages;
            ImageSize = imageSize;
            Source = source;
        }
    }
}
