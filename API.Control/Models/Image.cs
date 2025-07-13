using System.ComponentModel.DataAnnotations;

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


        // Profiles associados a image.
        public virtual ICollection<ProfileDeploy> Profiles { get; set; } = new List<ProfileDeploy>();
    }
}
