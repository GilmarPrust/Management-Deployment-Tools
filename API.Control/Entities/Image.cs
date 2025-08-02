using OperatingSystem = API.Control.Entities.Auxiliary.OperatingSystem;

namespace API.Control.Entities
{
    /// <summary>
    /// Representa uma imagem de sistema operacional, incluindo metadados e perfis associados.
    /// </summary>
    public class Image : BaseEntity
    {
        /// <summary>
        /// Nome da imagem do sistema operacional.
        /// </summary>
        [Required, StringLength(100)]
        public required string ImageName { get; init; }

        /// <summary>
        /// Descrição da imagem do sistema operacional.
        /// </summary>
        [StringLength(250)]
        public required string ImageDescription { get; init; }

        /// <summary>
        /// Índice da imagem no arquivo de origem.
        /// </summary>
        [Required, Range(1, 100, ErrorMessage = "O índice da imagem deve estar entre 1 e 100.")]
        public required int ImageIndex { get; init; }

        /// <summary>
        /// Identificador da edição da imagem.
        /// </summary>
        [Required, StringLength(50)]
        public required string EditionId { get; init; }

        /// <summary>
        /// Versão da imagem.
        /// </summary>
        [Required, StringLength(20)]
        public required string Version { get; init; }

        /// <summary>
        /// Idiomas suportados pela imagem.
        /// </summary>
        [Required]
        public required List<string> Languages { get; init; } = new();

        /// <summary>
        /// Tamanho da imagem em bytes.
        /// </summary>
        [Range(0, long.MaxValue)]
        public required long ImageSize { get; init; }

        /// <summary>
        /// Caminho de origem da imagem.
        /// </summary>
        [Required, StringLength(250)]
        public required string Source { get; init; }

        /// <summary>
        /// Chave estrangeira para o sistema operacional associado.
        /// </summary>
        [Required]
        public Guid OperatingSystemId { get; init; }

        /// <summary>
        /// Sistema operacional associado à imagem.
        /// </summary>
        public virtual OperatingSystem OperatingSystem { get; init; } = null!;

        /// <summary>
        /// Perfis de implantação associados à imagem.
        /// </summary>
        public virtual ICollection<DeployProfile> DeployProfiles { get; set; } = new List<DeployProfile>();

        /// <summary>
        /// Construtor vazio para o Entity Framework.
        /// </summary>
        public Image() { }

    }
}
