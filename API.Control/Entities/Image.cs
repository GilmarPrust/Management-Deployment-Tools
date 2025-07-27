namespace API.Control.Entities
{
    /// <summary>
    /// Representa uma imagem de sistema operacional, incluindo metadados e perfis associados.
    /// </summary>
    public class Image : _BaseEntity
    {

        [Required, StringLength(100)]
        public required string ImageName { get; set; }

        [StringLength(250)]
        public required string ImageDescription { get; set; }

        [Required, StringLength(20)]
        public required string ImageIndex { get; set; }

        [Required, StringLength(50)]
        public required string ShortName { get; set; }

        [Required, StringLength(50)]
        public required string EditionId { get; set; }

        [Required, StringLength(20)]
        public required string Version { get; set; }

        [Required]
        public required string[] Languages { get; set; }

        [Range(0, long.MaxValue)]
        public required long ImageSize { get; set; }

        [Required, StringLength(250)]
        public required string Source { get; set; }


        public Image() { }

        // Propriedade de navegação
        public virtual ICollection<DeployProfile> DeployProfiles { get; set; } = new List<DeployProfile>();
    }
}
