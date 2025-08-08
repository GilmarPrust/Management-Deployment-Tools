using API.DTOs.OperatingSystem;

namespace API.DTOs.Image
{
    /// <summary>
    /// DTO para leitura de imagem de sistema operacional.
    /// </summary>
    public class ImageReadDTO
    {
        /// <summary>
        /// Identificador único da imagem.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nome da imagem do sistema operacional.
        /// </summary>
        public string ImageName { get; init; } = string.Empty;

        /// <summary>
        /// Descrição da imagem do sistema operacional.
        /// </summary>
        public string ImageDescription { get; init; } = string.Empty;

        /// <summary>
        /// Índice da imagem no arquivo de origem.
        /// </summary>
        public int ImageIndex { get; init; }

        /// <summary>
        /// Identificador da edição da imagem.
        /// </summary>
        public string EditionId { get; init; } = string.Empty;

        /// <summary>
        /// Versão da imagem.
        /// </summary>
        public string Version { get; init; } = string.Empty;

        /// <summary>
        /// Idiomas suportados pela imagem.
        /// </summary>
        public IReadOnlyList<string> Languages { get; init; } = Array.Empty<string>();

        /// <summary>
        /// Tamanho da imagem em bytes.
        /// </summary>
        public long ImageSize { get; init; }

        /// <summary>
        /// Caminho de origem da imagem.
        /// </summary>
        public string Source { get; init; } = string.Empty;

        /// <summary>
        /// Indica se a imagem está habilitada.
        /// </summary>
        public bool Enabled { get; init; }

        /// <summary>
        /// Lista de IDs dos perfis de implantação associados à imagem.
        /// </summary>
        public IReadOnlyList<Guid> DeployProfileIds { get; init; } = Array.Empty<Guid>();

        /// <summary>
        /// Identificador do sistema operacional associado à imagem.
        /// </summary>
        public OperatingSystemReadDTO OperatingSystem { get; init; } = null!;
    }
}
