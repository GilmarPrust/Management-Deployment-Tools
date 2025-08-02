namespace API.Control.DTOs.Image
{
    /// <summary>
    /// DTO para criação de imagem de sistema operacional.
    /// </summary>
    public class ImageCreateDTO
    {
        /// <summary>
        /// Nome da imagem do sistema operacional.
        /// </summary>
        [Required, StringLength(100)]
        public string ImageName { get; init; } = string.Empty;

        /// <summary>
        /// Descrição da imagem do sistema operacional.
        /// </summary>
        [Required, StringLength(250)]
        public string ImageDescription { get; init; } = string.Empty;

        /// <summary>
        /// Índice da imagem no arquivo de origem.
        /// </summary>
        [Required, Range(1, 100)]
        public int ImageIndex { get; init; }

        /// <summary>
        /// Identificador da edição da imagem.
        /// </summary>
        [Required, StringLength(50)]
        public string EditionId { get; init; } = string.Empty;

        /// <summary>
        /// Versão da imagem.
        /// </summary>
        [Required, StringLength(20)]
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Idiomas suportados pela imagem.
        /// </summary>
        [Required]
        public List<string> Languages { get; init; } = new();

        /// <summary>
        /// Tamanho da imagem em bytes.
        /// </summary>
        [Required]
        public long ImageSize { get; init; } = 0;

        /// <summary>
        /// Caminho de origem da imagem.
        /// </summary>
        [Required, StringLength(250)]
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Identificador do sistema operacional associado à imagem.
        /// </summary>
        [Required]
        public Guid OperatingSystemId { get; init; }

    }
}
